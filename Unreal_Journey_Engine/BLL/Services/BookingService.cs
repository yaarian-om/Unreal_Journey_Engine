using AutoMapper;
using BLL.DTOs;
using DAL.Database.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class BookingService
    {

        #region C R U D Operation

        #region Get All Bookings

        public static List<BookingDTO> Get()
        {
            var data = RepoAccessFactory.Booking_Repo_Access().Get();
            if (data.Count > 0)
            {
                var mapper = MapperService<Booking, BookingDTO>.GetMapper();
                var TourDTO = mapper.Map<List<BookingDTO>>(data);
                return TourDTO;
            }
            else
            {
                return null;
            }
        }

        #endregion Get All Bookings

        #region Get Single Booking

        public static BookingDTO Get(int id)
        {
            var data = RepoAccessFactory.Booking_Repo_Access().Get(id);
            if (data != null)
            {
                var mapper = MapperService<Booking, BookingDTO>.GetMapper();
                var TourDTO = mapper.Map<BookingDTO>(data);
                return TourDTO;
            }
            else
            {
                return null;
            }
        }

        #endregion Get Single Booking

        #region Create Booking

        public static bool Create(BookingDTO dto)
        {
            // Convert to Booking, from Booking_DTO
            if (dto != null)
            {
                var mapper = MapperService<BookingDTO, Booking>.GetMapper();
                var Tour_Data = mapper.Map<Booking>(dto);

                // Get Tour_Package info by Tour_ID
                // Get Tourist_Profile info by Tourist_ID

                var tour_info = RepoAccessFactory.Tour_Package_Repo_Access().Get(dto.Tour_ID);
                var tourist_info = RepoAccessFactory.Tourist_Profile_Repo_Access().Get(dto.Tourist_ID);
                var user_info = RepoAccessFactory.User_Repo_Access().Get(tourist_info.User_ID);
                var invoice_page = $@"
                    <!DOCTYPE html>
                    <html lang=""en"">
                    <head>
                        <meta charset=""UTF-8"">
                        <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                        <title>Tourist Invoice</title>
                        <style>
                            /* Common styles for all screen sizes */
                            body {{
                                font-family: Arial, sans-serif;
                                margin: 0;
                                padding: 0;
                                background-color: #f4f4f4;
                                color: #333;
                            }}
                            .container {{
                                max-width: 100%;
                                margin: 20px auto;
                                padding: 20px;
                                background-color: #fff;
                                box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                                display: grid;
                                grid-template-columns: 1fr 1fr;
                                gap: 20px;
                                align-items: center; /* Vertically align content */
                                grid-template-areas:
                                    ""header header""
                                    ""info-column agency-name""
                                    ""data-show data-show""
                                    ""footer footer"";
                            }}
                            .header {{
                                text-align: center;
                                margin-bottom: 20px;
                                grid-column: span 2; /* Full-width header */
                            }}
                            .info-column {{
                                grid-area: info-column;
                                display: flex;
                                flex-direction: column;
                            }}
                            .info-item {{
                                margin-bottom: 10px;
                            }}
                            .info-label {{
                                font-weight: bold;
                            }}
                            .table-container {{
                                width: 100%;
                                border-collapse: collapse;
                                margin-top: 20px;
                                text-align: center; /* Center table content */
                                grid-area: data-show;
                            }}
                            .table-container th, .table-container td {{
                                padding: 10px;
                                text-align: left;
                                border-bottom: 1px solid #ddd;
                            }}
                            .footer {{
                                text-align: center;
                                font-size: 12px;
                                color: #888;
                                margin-top: 20px;
                                grid-column: span 2; /* Full-width footer */
                                grid-area: footer;
                            }}
                            .agency-name {{
                                font-size: 20px;
                                font-weight: bold;
                                color: black;
                                text-align: right;
                                grid-area: agency-name;
                            }}
                            .agency-logo {{
                                max-width: 60%;
                                height: auto;
                                float: right;
                            }}
                            /* Styles for A4 paper size */
                            @media print {{
                                .container {{
                                    max-width: 100%;
                                    margin: 0;
                                    padding: 0;
                                    box-shadow: none;
                                    page-break-before: always;
                                }}
                                .agency-name {{
                                    font-size: 30px;
                                }}
                                .table-container th, .table-container td {{
                                    border-bottom: 1px solid #ddd;
                                }}
                                .footer {{
                                    display: none;
                                }}
                            }}
                        </style>
                    </head>
                    <body>
                        <div class=""container"">
                            <div class=""header"">
                                <h1>Tourist Invoice</h1>
                                <p>Thank you for choosing Unreal Journey!</p>
                            </div>
                            <div class=""info-column"">
                                <div class=""info-item"">
                                    <span class=""info-label"">Tour Title:</span>
                                    {tour_info.Title}
                                </div>
                                <div class=""info-item"">
                                    <span class=""info-label"">Tourist Name:</span>
                                    {tourist_info.Name}
                                </div>
                                <div class=""info-item"">
                                    <span class=""info-label"">Tourist Phone:</span>
                                    {tourist_info.Phone}
                                </div>
                            </div>
                            <div class=""agency-name"">
                                <img src=""https://raw.githubusercontent.com/yaarian-om/SERVER/f6ab20118be62bd601e248a6a26412bfb7ed22d2/1010110010/Unreal_Journey_Engine/Logo.svg"" alt=""Unreal Journey Logo"" class=""agency-logo"">
                            </div>
                            <table class=""table-container"">
                                <tr>
                                    <th>Description</th>
                                    <th>Tour Location</th>
                                    <th>Tour Duration</th>
                                    <th>Total</th>
                                </tr>
                                <tr>
                                    <td>Tour Package</td>
                                    <td>{tour_info.Location}</td>
                                    <td>{tour_info.Duration}</td>
                                    <td>{tour_info.Cost}(৳)BDT</td>
                                </tr>
                                <!-- Add more items if needed -->
                            </table>
                            <div class=""footer"">
                                <p>For any inquiries, please contact us at support@unrealjourney.com</p>
                            </div>
                        </div>
                    </body>
                    </html>



                ";

                var decision = InvoiceService.Create_and_Send_Invoice(user_info.Email, invoice_page);
                if (decision)
                {
                    return RepoAccessFactory.Booking_Repo_Access().Create(Tour_Data);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        #endregion Create Booking

        #region Delete a Booking

        public static bool Delete(int id)
        {
            if (id > 0)
            {
                return RepoAccessFactory.Booking_Repo_Access().Delete(id);
            }
            else
            {
                return false;
            }
        }

        #endregion Delete a Booking

        #region Update Booking

        public static bool Update(BookingDTO dto)
        {
            if (dto != null)
            {
                var mapper = MapperService<BookingDTO, Booking>.GetMapper();
                var Tour_Data = mapper.Map<Booking>(dto);
                return RepoAccessFactory.Booking_Repo_Access().Update(Tour_Data);
            }
            else
            {
                return false;
            }
        }

        #endregion Update Booking

        #endregion C R U D Operation


        // Feature Api Needed




    }
}
