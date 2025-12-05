// DAL/BookingDAL.cs
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HotelBooking.Models;

namespace HotelBooking.DAL
{
    public class BookingDAL
    {
        public List<Booking> GetAllBookings()
        {
            List<Booking> bookings = new List<Booking>();
            string query = @"SELECT B.*, RT.TypeName, RT.Price 
                           FROM Bookings B 
                           INNER JOIN RoomTypes RT ON B.RoomTypeID = RT.RoomTypeID 
                           ORDER BY B.BookingDate DESC";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);

            foreach (DataRow row in dt.Rows)
            {
                bookings.Add(MapBooking(row));
            }
            return bookings;
        }

        public Booking GetBookingByID(int id)
        {
            string query = @"SELECT B.*, RT.TypeName, RT.Price 
                           FROM Bookings B 
                           INNER JOIN RoomTypes RT ON B.RoomTypeID = RT.RoomTypeID 
                           WHERE B.BookingID = @ID";
            SqlParameter[] parameters = { new SqlParameter("@ID", id) };
            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

            if (dt.Rows.Count > 0)
            {
                return MapBooking(dt.Rows[0]);
            }
            return null;
        }

        public bool Insert(Booking booking)
        {
            string query = @"INSERT INTO Bookings (CustomerName, Email, Phone, RoomTypeID, 
                           CheckInDate, CheckOutDate, NumberOfGuests, TotalPrice, SpecialRequests, Status) 
                           VALUES (@CustomerName, @Email, @Phone, @RoomTypeID, @CheckInDate, 
                           @CheckOutDate, @NumberOfGuests, @TotalPrice, @SpecialRequests, @Status)";
            SqlParameter[] parameters = {
                new SqlParameter("@CustomerName", booking.CustomerName),
                new SqlParameter("@Email", booking.Email),
                new SqlParameter("@Phone", booking.Phone),
                new SqlParameter("@RoomTypeID", booking.RoomTypeID),
                new SqlParameter("@CheckInDate", booking.CheckInDate),
                new SqlParameter("@CheckOutDate", booking.CheckOutDate),
                new SqlParameter("@NumberOfGuests", booking.NumberOfGuests),
                new SqlParameter("@TotalPrice", booking.TotalPrice),
                new SqlParameter("@SpecialRequests", booking.SpecialRequests ?? ""),
                new SqlParameter("@Status", booking.Status)
            };
            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool UpdateStatus(int bookingID, string status)
        {
            string query = "UPDATE Bookings SET Status = @Status WHERE BookingID = @ID";
            SqlParameter[] parameters = {
                new SqlParameter("@ID", bookingID),
                new SqlParameter("@Status", status)
            };
            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool Delete(int id)
        {
            string query = "DELETE FROM Bookings WHERE BookingID = @ID";
            SqlParameter[] parameters = { new SqlParameter("@ID", id) };
            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        private Booking MapBooking(DataRow row)
        {
            return new Booking
            {
                BookingID = Convert.ToInt32(row["BookingID"]),
                CustomerName = row["CustomerName"].ToString(),
                Email = row["Email"].ToString(),
                Phone = row["Phone"].ToString(),
                RoomTypeID = Convert.ToInt32(row["RoomTypeID"]),
                CheckInDate = Convert.ToDateTime(row["CheckInDate"]),
                CheckOutDate = Convert.ToDateTime(row["CheckOutDate"]),
                NumberOfGuests = Convert.ToInt32(row["NumberOfGuests"]),
                TotalPrice = Convert.ToDecimal(row["TotalPrice"]),
                SpecialRequests = row["SpecialRequests"].ToString(),
                Status = row["Status"].ToString(),
                BookingDate = Convert.ToDateTime(row["BookingDate"]),
                RoomType = new RoomType
                {
                    TypeName = row["TypeName"].ToString(),
                    Price = Convert.ToDecimal(row["Price"])
                }
            };
        }
    }
}