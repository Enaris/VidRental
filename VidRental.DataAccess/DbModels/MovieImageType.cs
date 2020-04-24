using System;
using System.Collections.Generic;
using System.Text;

namespace VidRental.DataAccess.DbModels
{
    public static class MovieImageType
    {
        public static string ToStr(MovieImageTypeEnum type) => type.ToString();

        public static MovieImageTypeEnum ToEnum(string type) => Enum.Parse<MovieImageTypeEnum>(type);

        public static string Cover => ToStr(MovieImageTypeEnum.Cover);
        public static string Thumbnail => ToStr(MovieImageTypeEnum.Thumbnail);
        public static string Image => ToStr(MovieImageTypeEnum.Image);
    }

    public enum MovieImageTypeEnum
    {
        Cover, 
        Thumbnail,
        Image
    }
}
