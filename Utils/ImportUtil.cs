using ClientAddressManager.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ClientAddressManager
{
    public static class ImportUtil<T>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Result with data or error; null if dialog was cancelled</returns>
        public static Result<List<T>> ImportModelsFromFileDialog()
        {
            var filePath = GetFilePathFromFileDialog();
            if (filePath == null)
                return null;

            var json = GetJsonFromFile(filePath);
            if (json == null)
                return new Result<List<T>>(ResultCode.ERROR, default, Strings.ERROR_READ_FILE); 

            var objects = DeserializeJson(json);
            if (objects == null)
                return new Result<List<T>>(ResultCode.ERROR, default, Strings.ERROR_PARSE_JSON);

            var result = new Result<List<T>>(ResultCode.SUCCESS, objects, null);
            return result;
        }


        public static string GetFilePathFromFileDialog()
        {
            using (var fileDialog = new OpenFileDialog())
            {
                fileDialog.Reset();

                var initialDirectory = AppDomain.CurrentDomain.BaseDirectory;
                fileDialog.InitialDirectory = initialDirectory;

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    return fileDialog.FileName;
                }
            }

            return null;
        }


        public static string GetJsonFromFile(string path)
        {
            string jsonStr = null;

            try
            {
                if (path != null && File.Exists(path))
                    jsonStr = File.ReadAllText(path, Encoding.UTF8);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return jsonStr;
        }


        public static List<T> DeserializeJson(string jsonStr)
        {
            List<T> list = null;

            try
            {
                if (jsonStr != null)
                    list = JsonConvert.DeserializeObject<List<T>>(jsonStr);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return list;
        }




    }
}
