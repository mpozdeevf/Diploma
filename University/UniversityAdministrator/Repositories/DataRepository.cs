using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using UniversityAdministrator.Models;
using UniversityData.Models;

namespace UniversityAdministrator.Repositories
{
    public static class DataRepository
    {
        private static readonly HttpClient Client = new HttpClient();

        private const string InstitutesApiAddress = "http://localhost:5000/api/university/institutes";
        private const string DepartmentsApiAddress = "http://localhost:5000/api/university/departments";
        private const string GroupsApiAddress = "http://localhost:5000/api/university/groups";
        private const string StudentsApiAddress = "http://localhost:5000/api/university/students";

        #region GetData

        public static IEnumerable<Institute> GetDbInstitutes()
        {
            try
            {
                var response = Client.GetAsync(InstitutesApiAddress).Result;
                response.EnsureSuccessStatusCode();
                var responseBody = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<Institute>>(responseBody);
            }
            catch (Exception e)
            {
                return new List<Institute>();
            }
        }

        public static IEnumerable<Department> GetDbDepartments()
        {
            try
            {
                var response = Client.GetAsync(DepartmentsApiAddress).Result;
                response.EnsureSuccessStatusCode();
                var responseBody = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<Department>>(responseBody);
            }
            catch (Exception e)
            {
                return new List<Department>();
            }
        }

        public static IEnumerable<Group> GetDbGroups()
        {
            try
            {
                var response = Client.GetAsync(GroupsApiAddress).Result;
                response.EnsureSuccessStatusCode();
                var responseBody = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<Group>>(responseBody);
            }
            catch (Exception e)
            {
                return new List<Group>();
            }
        }

        public static IEnumerable<Student> GetDbStudents()
        {
            try
            {
                var response = Client.GetAsync(StudentsApiAddress).Result;
                response.EnsureSuccessStatusCode();
                var responseBody = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<Student>>(responseBody);
            }
            catch (Exception e)
            {
                return new List<Student>();
            }
        }

        #endregion

        #region DeleteStudent

        public static void DeleteStudent(string studentNumber)
        {
            try
            {
                var response = Client.DeleteAsync($"{StudentsApiAddress}?id={studentNumber}").Result;
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                // ignored
            }
        }

        #endregion
    }
}