using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarSelector.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

namespace CarSelector.Views.Home
{
    public class CarController : Controller
    {
        SqliteConnection databaseConnection;

        public CarController(SqliteConnection db)
        {
            databaseConnection = db;
        }

        private Car ReadCarModel(SqliteDataReader reader)
        {
            Car car = new Car();
            car.Make = reader.GetString(reader.GetOrdinal("Make"));
            car.Model = reader.GetString(reader.GetOrdinal("Model"));
            car.Color = reader.GetString(reader.GetOrdinal("Color"));
            car.Year = reader.GetInt16(reader.GetOrdinal("Year"));
            car.Price = reader.GetDecimal(reader.GetOrdinal("Price"));
            car.TCC = reader.GetFloat(reader.GetOrdinal("TCC"));
            car.MPG = reader.GetFloat(reader.GetOrdinal("MPG"));
            return car;
        }

        public IActionResult New()
        {
            var cars = new List<Car>();
            string SQL = "SELECT * FROM Cars ORDER BY Year DESC";
            SqliteCommand command = new SqliteCommand(SQL, databaseConnection);
            SqliteDataReader reader = null;

            try
            {
                reader = command.ExecuteReader();

                while (reader.Read())
                    cars.Add(ReadCarModel(reader));
            }
            catch (Exception exception)
            {
                return View("Home/Error", exception);
            }
            finally
            {
                if (reader != null) reader.Close();
                command.Dispose();
            }
            return View(cars);
        }

        public IActionResult Model()
        {
            var cars = new List<Car>();
            string SQL = "SELECT * FROM Cars ORDER BY Model";
            SqliteCommand command = new SqliteCommand(SQL, databaseConnection);
            SqliteDataReader reader = null;

            try
            {
                reader = command.ExecuteReader();

                while (reader.Read())
                    cars.Add(ReadCarModel(reader));
            }
            catch (Exception exception)
            {
                return View("Home/Error", exception);
            }
            finally
            {
                if (reader != null) reader.Close();
                command.Dispose();
            }
            return View(cars);
        }

        public IActionResult Price()
        {
            var cars = new List<Car>();
            string SQL = "SELECT * FROM Cars ORDER BY Price";
            SqliteCommand command = new SqliteCommand(SQL, databaseConnection);
            SqliteDataReader reader = null;

            try
            {
                reader = command.ExecuteReader();

                while (reader.Read())
                    cars.Add(ReadCarModel(reader));
            }
            catch (Exception exception)
            {
                return View("Home/Error", exception);
            }
            finally
            {
                if (reader != null) reader.Close();
                command.Dispose();
            }
            return View(cars);
        }

        public IActionResult Value()
        {
            var cars = new List<Car>();
            string SQL = "SELECT *, (TCC/Price)*20000 AS Value FROM Cars ORDER BY Value DESC";
            SqliteCommand command = new SqliteCommand(SQL, databaseConnection);
            SqliteDataReader reader = null;

            try
            {
                reader = command.ExecuteReader();

                while (reader.Read())
                    cars.Add(ReadCarModel(reader));
            }
            catch (Exception exception)
            {
                return View("Home/Error", exception);
            }
            finally
            {
                if (reader != null) reader.Close();
                command.Dispose();
            }
            return View(cars);
        }

        public IActionResult Fuel(float? distance)
        {
            if (!distance.HasValue) distance = 10.0f;
            ViewData["Distance"] = distance;

            var cars = new List<Car>();
            string SQL = "SELECT * FROM Cars ORDER BY MPG DESC";
            SqliteCommand command = new SqliteCommand(SQL, databaseConnection);
            SqliteDataReader reader = null;

            try
            {
                reader = command.ExecuteReader();

                while (reader.Read())
                    cars.Add(ReadCarModel(reader));
            }
            catch (Exception exception)
            {
                return View("Home/Error", exception);
            }
            finally
            {
                if (reader != null) reader.Close();
                command.Dispose();
            }
            return View(cars);
        }

        public IActionResult Random()
        {
            var cars = new List<Car>();
            string SQL = "SELECT * FROM Cars";
            SqliteCommand command = new SqliteCommand(SQL, databaseConnection);
            SqliteDataReader reader = null;

            try
            {
                reader = command.ExecuteReader();

                while (reader.Read())
                    cars.Add(ReadCarModel(reader));
            }
            catch (Exception exception)
            {
                return View("Home/Error", exception);
            }
            finally
            {
                if (reader != null) reader.Close();
                command.Dispose();
            }

            Random rand = new Random();
            var randomID = rand.Next(cars.Count);
            return View(cars[randomID]);
        }

        public IActionResult MPG()
        {
            var yearMPGs = new List<YearMPG>();
            string SQL = "SELECT Year, avg(MPG) FROM Cars GROUP BY Year ORDER BY Year DESC";
            SqliteCommand command = new SqliteCommand(SQL, databaseConnection);
            SqliteDataReader reader = null;

            try
            {
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var yearMPG = new YearMPG();
                    yearMPG.Year = reader.GetInt16(0);
                    yearMPG.MPG = reader.GetFloat(1);
                    yearMPGs.Add(yearMPG);
                }
            }
            catch (Exception exception)
            {
                return View("Home/Error", exception);
            }
            finally
            {
                if (reader != null) reader.Close();
                command.Dispose();
            }
            return View(yearMPGs);
        }
    }
}