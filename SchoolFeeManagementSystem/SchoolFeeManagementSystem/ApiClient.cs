using Newtonsoft.Json;
using SchoolFeeManagemetSystem.API.DTOs;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using static SchoolFeeManagemetSystem.API.DTOs.FeeCategoryDTOs;
using static SchoolFeeManagemetSystem.API.DTOs.FeeStructureDTOs;
using static SchoolFeeManagemetSystem.API.DTOs.PaymentDTOs;
using static SchoolFeeManagemetSystem.API.DTOs.ReportDTOs;
using static SchoolFeeManagemetSystem.API.DTOs.StaffDTOs;
using static SchoolFeeManagemetSystem.API.DTOs.StudentDTOs;
using static SchoolFeeManagemetSystem.API.DTOs.UserDTOs;
namespace SchoolFeeManagementSystem
{
    public class ApiClient
    {
        private readonly HttpClient _http;
        private readonly JsonSerializerOptions _options;

        public ApiClient()
        {
            _http = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7165/api/")
            };

            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        // ================= COMMON METHODS =================

        private async Task<T> HandleResponse<T>(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"API Error ({response.StatusCode}): {error}");
            }

            return await response.Content.ReadFromJsonAsync<T>(_options);
        }

        private async Task HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"API Error ({response.StatusCode}): {error}");
            }
        }

        // ================= GENERIC CRUD =================

        public async Task<List<T>> GetAllAsync<T>(string endpoint)
        {
            var response = await _http.GetAsync(endpoint);
            return await HandleResponse<List<T>>(response);
        }

        public async Task<T> GetByIdAsync<T>(string endpoint, int id)
        {
            var response = await _http.GetAsync($"{endpoint}/{id}");
            return await HandleResponse<T>(response);
        }

        public async Task<T> PostAsync<T>(string endpoint, object data)
        {
            var response = await _http.PostAsJsonAsync(endpoint, data);
            return await HandleResponse<T>(response);
        }

        public async Task PutAsync(string endpoint, object data)
        {
            var response = await _http.PutAsJsonAsync(endpoint, data);
            await HandleResponse(response);
        }

        public async Task DeleteAsync(string BaseAddress, int id)
        {
            var response = await _http.DeleteAsync($"{BaseAddress}/{id}");
            await HandleResponse(response);
        }

        // ================= DASHBOARD =================

        public async Task<DashboardDTO> GetDashboardAsync()
        {
            var response = await _http.GetAsync("Dashboard");
            return await HandleResponse<DashboardDTO>(response);
        }

        // ================= STUDENT METHODS =================

        // GET ALL STUDENTS
        public async Task<List<StudentDTO>> GetStudentsAsync()
        {
            var response = await _http.GetAsync("Student");
            return await HandleResponse<List<StudentDTO>>(response);
        }

        // GET STUDENT BY ID
        public async Task<StudentDTO> GetStudentByIdAsync(int id)
        {
            var response = await _http.GetAsync($"Student/{id}");
            return await HandleResponse<StudentDTO>(response);
        }

        // CREATE STUDENT
        public async Task<StudentDTO> CreateStudentAsync(CreateStudentDTO dto)
        {
            var response = await _http.PostAsJsonAsync("Student", dto);
            return await HandleResponse<StudentDTO>(response);
        }

        // UPDATE STUDENT
        public async Task UpdateStudentAsync(UpdateStudentDTO dto)
        {
            var response = await _http.PutAsJsonAsync("Student", dto);
            await HandleResponse(response);
        }

        // DELETE STUDENT
        public async Task DeleteStudentAsync(int id)
        {
            var response = await _http.DeleteAsync($"Student/{id}");
            await HandleResponse(response);
        }

        // ================= STAFF =================
        public async Task<List<StaffDtos>> GetAllStaffAsync()
        {
            var response = await _http.GetAsync("Staff");

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<StaffDtos>>(json);
        }

        // CREATE STAFF
        public async Task<bool> CreateStaffAsync(CreateStaffDto dto)
        {
            var json = JsonConvert.SerializeObject(dto);

            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            var response = await _http.PostAsync(
                "Staff",
                content);

            return response.IsSuccessStatusCode;
        }

        // UPDATE STAFF
        public async Task<bool> UpdateStaffAsync(UpdateStaffDto dto)
        {
            var json = JsonConvert.SerializeObject(dto);

            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            var response = await _http.PutAsync(
                "Staff",
                content);

            return response.IsSuccessStatusCode;
        }

        // DELETE STAFF
        public async Task<bool> DeleteStaffAsync(int id)
        {
            var response = await _http.DeleteAsync(
                "Staff/" + id);

            return response.IsSuccessStatusCode;
        }
        // ================= FEE CATEGORY =================
        public async Task<List<FeeCategoryDTO>> GetCategoriesAsync()
        {
            var response = await _http.GetAsync("FeeCategory");

            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<List<FeeCategoryDTO>>();
        }
        public async Task<FeeCategoryDTO> CreateCategoryAsync(CreateFeeCategoryDTO dto)
        {
            var response = await _http.PostAsJsonAsync("FeeCategory", dto);

            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<FeeCategoryDTO>();
        }
        public async Task UpdateCategoryAsync(UpdateFeeCategoryDTO dto)
        {
           
            var response = await _http.PutAsJsonAsync($"FeeCategory/{dto.Id}", dto);

            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());
        }
        public async Task DeleteCategoryAsync(int id)
        {
            var response = await _http.DeleteAsync($"FeeCategory/{id}");

            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());
        }
        // ================= FEE STRUCTURE =================

        // GET ALL
        public async Task<List<FeeStructureDTO>> GetFeeStructuresAsync()
        {
            return await _http.GetFromJsonAsync<List<FeeStructureDTO>>("FeeStructure");
        }

        // CREATE
        public async Task CreateFeeStructureAsync(CreateFeeStructureDTO dto)
        {
            var res = await _http.PostAsJsonAsync("FeeStructure", dto);
            res.EnsureSuccessStatusCode();
        }

        // UPDATE (IMPORTANT FIX)
        public async Task UpdateFeeStructureAsync(UpdateFeeStructureDTO dto)
        {
            var res = await _http.PutAsJsonAsync($"FeeStructure/{dto.Id}", dto);
            res.EnsureSuccessStatusCode();
        }

        // DELETE
        public async Task DeleteFeeStructureAsync(int id)
        {
            var res = await _http.DeleteAsync($"FeeStructure/{id}");
            res.EnsureSuccessStatusCode();
        }

        // ================= PAYMENT =================

        public async Task<List<PaymentDTO>> GetPaymentsAsync()
        {
            return await GetAllAsync<PaymentDTO>("Payment");
        }

        public async Task CreatePaymentAsync(PaymentDTO dto)
        {
            await PostAsync<object>("Payment", dto);
        }

        public async Task UpdatePaymentAsync(PaymentDTO dto)
        {
            await PutAsync("Payment", dto);
        }

        public async Task DeletePaymentAsync(int id)
        {
            await DeleteAsync("Payment", id);
        }



        // ================= REPORT =================

        public async Task<List<ReportDTO>> GetReportsAsync(DateTime from,DateTime to,string className,string category)
        {
            var url = $"Reports?from={from:yyyy-MM-dd}" +$"&to={to:yyyy-MM-dd}" +$"&className={className}" + $"&category={category}";
            var response = await _http.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"API Error: {error}");
            }

            return await response.Content.ReadFromJsonAsync<List<ReportDTO>>();
        }

        // ================= LOGIN =================

        public async Task<UserDTO?> LoginAsync(LoginDTO dto)
        {
            try
            {
                var json = JsonConvert.SerializeObject(dto);

                var content = new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");

                var response =
                    await _http.PostAsync("User/login", content);

                // ERROR
                if (!response.IsSuccessStatusCode)
                {
                    var error =
                        await response.Content.ReadAsStringAsync();

                    MessageBox.Show(error,
                        "Login Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return null;
                }

                // SUCCESS
                var result =
                    await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<UserDTO>(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "API Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return null;
            }
        }

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            var response =
                await _http.GetAsync($"User/{id}");

            if (!response.IsSuccessStatusCode)
                return null;

            var json =
                await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<UserDTO>(json);
        }

        public async Task<bool> UpdateAccountAsync(UserDTO user)
        {
            var response = await _http.PutAsJsonAsync(  "api/User/update", user);

            return response.IsSuccessStatusCode;
        }
    }
}

