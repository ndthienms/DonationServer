using DonationAppDemo.DAL;
using DonationAppDemo.DTOs;
using DonationAppDemo.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DonationAppDemo.Services
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly IAccountDal _accountDal;
        private readonly IOrganiserDal _organiserDal;
        private readonly IDonorDal _donorDal;
        private readonly ITransactionDal _transactionDal;
        private readonly IUtilitiesService _utilitiesService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public UserAuthenticationService(IAccountDal accountDal,
            IOrganiserDal organiserDal,
            IDonorDal donorDal,
            ITransactionDal transactionDal,
            IUtilitiesService utilitiesService,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
        {
            _accountDal = accountDal;
            _organiserDal = organiserDal;
            _donorDal = donorDal;
            _transactionDal = transactionDal;
            _utilitiesService = utilitiesService;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }
        public async Task<string> CheckExistedUser(string phoneNum)
        {
            // Check existed account
            var user = await _accountDal.Get(phoneNum);
            if (user != null)
            {
                throw new Exception("TThis account is existed");
            }

            // Send otp for phone number verification
            return await _utilitiesService.TwilioSendCodeSms("+84" + phoneNum.Substring(1));
        }
        public async Task<OrganiserDto> SignUpOrganiser(SignUpOrganiserDto signUpOrganiserDto)
        {
            // Verify otp
            var verified = await _utilitiesService.TwilioVerifyCodeSms(signUpOrganiserDto.Code, "+84" + signUpOrganiserDto.PhoneNum.Substring(1));
            if (verified == null || verified == false)
            {
                throw new Exception("Otp is not matched");
            }

            // Hash password
            var hashSaltResult = _utilitiesService.HMACSHA512(signUpOrganiserDto.Password);

            // Add certification image to cloudinary
            var uploadImageResult = await _utilitiesService.CloudinaryUploadPhotoAsync(signUpOrganiserDto.CertificationFile);
            if(uploadImageResult.Error != null)
            {
                throw new Exception("Cannot upload certidication image");
            }

            // OrganiserDto
            var organiserDto = new OrganiserDto()
            {
                PhoneNum = signUpOrganiserDto.PhoneNum,
                Name = signUpOrganiserDto.Name,
                Gender = signUpOrganiserDto.Gender,
                Dob = signUpOrganiserDto.Dob,
                Email = signUpOrganiserDto.Email,
                Address = signUpOrganiserDto.Address,
                CertificationSrc = uploadImageResult.SecureUrl.AbsoluteUri,
                Description = signUpOrganiserDto.Description
            };

            // AccountDto
            var accountDto = new AccountDto()
            {
                PhoneNum = signUpOrganiserDto.PhoneNum,
                PasswordHash = hashSaltResult.hashedCode,
                PasswordSalt = hashSaltResult.keyCode,
                Role = "organiser",
                Disabled = true
            };

            // Add to account and organiser table in db
            var transactionResult = await _transactionDal.AccountOrganiser(accountDto, organiserDto, uploadImageResult.PublicId);
            if (transactionResult == false)
            {
                await _utilitiesService.CloudinaryDeletePhotoAsync(uploadImageResult.PublicId);
                throw new Exception("Sign up failed");
            }
            return organiserDto;
        }
        public async Task<DonorDto> SignUpDonor(SignUpDonorDto signUpDonorDto)
        {
            // Verify otp
            var verified = await _utilitiesService.TwilioVerifyCodeSms(signUpDonorDto.Code, "+84" + signUpDonorDto.PhoneNum.Substring(1));
            if (verified == null || verified == false)
            {
                throw new Exception("Otp is not matched");
            }

            // Hash password
            var hashSaltResult = _utilitiesService.HMACSHA512(signUpDonorDto.Password);

            // DonorDto
            var donorDto = new DonorDto()
            {
                PhoneNum = signUpDonorDto.PhoneNum,
                Name = signUpDonorDto.Name,
                Gender = signUpDonorDto.Gender,
                Dob = signUpDonorDto.Dob,
                Email = signUpDonorDto.Email,
                Address = signUpDonorDto.Address
            };

            // AccountDto
            var accountDto = new AccountDto()
            {
                PhoneNum = signUpDonorDto.PhoneNum,
                PasswordHash = hashSaltResult.hashedCode,
                PasswordSalt = hashSaltResult.keyCode,
                Role = "donor",
                Disabled = false
            };

            // Add to account and organiser table in db
            var transactionResult = await _transactionDal.AccountDonor(accountDto, donorDto);
            if (transactionResult == false)
            {
                throw new Exception("Sign up failed");
            }
            return donorDto;
        }
        public async Task<string> SignIn(SignInDto signInDto)
        {
            // Check account condition
            var user = await _accountDal.Get(signInDto.PhoneNum);
            if (user == null)
            {
                throw new Exception("Account does not exist");
            }
            if (user.disabled == true)
            {
                throw new Exception("Account has been locked or not approved");
            }
            if (!_utilitiesService.MatchCodeHash(signInDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new Exception("Password is not corrected");
            }

            // Get information of user and create claims
            var authClaims = new List<Claim>();
            if (user.Role == "organiser")
            {
                var userInformation = await _organiserDal.GetByPhoneNum(user.PhoneNum);

                // Create claims
                authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.PhoneNum),
                    new Claim(ClaimTypes.Name, userInformation.Name),
                    new Claim(type: "Id", value: userInformation.Id.ToString()),
                    new Claim(type: "AvaSrc", value: userInformation.AvaSrc != null ? userInformation.AvaSrc : "0"),
                    new Claim(type: "CertificationSrc", value: userInformation.CertificationSrc),
                    new Claim(ClaimTypes.Role, user.Role)
                };
            }
            else // donor
            {
                var userInformation = await _donorDal.GetByPhoneNum(user.PhoneNum);

                // Create claims
                authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.PhoneNum),
                    new Claim(ClaimTypes.Name, userInformation.Name),
                    new Claim(type: "Id", value: userInformation.Id.ToString()),
                    new Claim(type: "AvaSrc", value: userInformation.AvaSrc != null ? userInformation.AvaSrc : "0"),
                    new Claim(ClaimTypes.Role, user.Role)
                };
            }

            // Create Jwt
            var signInKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(7),
                claims: authClaims,
                signingCredentials: new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256Signature)
                );
            var tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenAsString;
        }
        public async Task<bool> UpdateApprovementOrganiser(string phoneNum, int organiserId)
        {
            // Get current user
            var handler = new JwtSecurityTokenHandler();
            string authHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            authHeader = authHeader.Replace("Bearer ", "");
            var jsonToken = handler.ReadToken(authHeader);
            var tokenS = handler.ReadJwtToken(authHeader) as JwtSecurityToken;
            var currentUserId = tokenS.Claims.First(claim => claim.Type == "Id").Value.ToString();

            // Approve organiser
            var result = await _transactionDal.UpdateAccountOrganiserApprovement(organiserId, Int32.Parse(currentUserId), phoneNum);
            if (!result)
            {
                throw new Exception($"Cannot approve organiser {organiserId}");
            }

            return result;
        }
    }
}
