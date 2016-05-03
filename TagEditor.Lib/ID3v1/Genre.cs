﻿using System;
using System.Linq;

namespace TagEditor.Lib.ID3v1
{
    public static class Genre
    {
        public static string ToFriendlyString(this Type type)
        {    
            return type == Type.None 
                ? type.ToString()
                : typeStrings.ElementAt((int) type);
        }

        public static Type FromString(string value)
        {
            var id = Array.IndexOf(typeStrings, value);

            return id != -1 ? (Type) id : Type.None;
        }

        public enum Type
        {
            Blues,
            ClassicRock,
            Country,
            Dance,
            Disco,
            Funk,
            Grunge,
            HipHop,
            Jazz,
            Metal,
            NewAge,
            Oldies,
            Other,
            Pop,
            RnB,
            Rap,
            Reggae,
            Rock,
            Techno,
            Industrial,
            Alternative,
            Ska,
            DeathMetal,
            Pranks,
            Soundtrack,
            EuroTechno,
            Ambient,
            TripHop,
            Vocal,
            JazzFunk,
            Fusion,
            Trance,
            Classical,
            Instrumental,
            Acid,
            House,
            Game,
            SoundClip,
            Gospel,
            Noise,
            AlternRock,
            Bass,
            Soul,
            Punk,
            Space,
            Meditative,
            InstrumentalPop,
            InstrumentalRock,
            Ethnic,
            Gothic,
            Darkwave,
            TechnoIndustrial,
            Electronic,
            PopFolk,
            Eurodance,
            Dream,
            SouthernRock,
            Comedy,
            Cult,
            Gangsta,
            Top40,
            ChristianRap,
            PopFunk,
            Jungle,
            NativeAmerican,
            Cabaret,
            NewWave,
            Psychadelic,
            Rave,
            Showtunes,
            Trailer,
            LoFi,
            Tribal,
            AcidPunk,
            AcidJazz,
            Polka,
            Retro,
            Musical,
            RocknRoll,
            HardRock,
            None = 255
        }

        private static readonly string[] typeStrings =
        {
            "Blues",
            "Classic Rock",
            "Country",
            "Dance",
            "Disco",
            "Funk",
            "Grunge",
            "Hip-Hop",
            "Jazz",
            "Metal",
            "New Age",
            "Oldies",
            "Other",
            "Pop",
            "R&B",
            "Rap",
            "Reggae",
            "Rock",
            "Techno",
            "Industrial",
            "Alternative",
            "Ska",
            "Death Metal",
            "Pranks",
            "Soundtrack",
            "Euro-Techno",
            "Ambient",
            "Trip-Hop",
            "Vocal",
            "Jazz+Funk",
            "Fusion",
            "Trance",
            "Classical",
            "Instrumental",
            "Acid",
            "House",
            "Game",
            "Sound Clip",
            "Gospel",
            "Noise",
            "AlternRock",
            "Bass",
            "Soul",
            "Punk",
            "Space",
            "Meditative",
            "Instrumental Pop",
            "Instrumental Rock",
            "Ethnic",
            "Gothic",
            "Darkwave",
            "Techno-Industrial",
            "Electronic",
            "Pop-Folk",
            "Eurodance",
            "Dream",
            "Southern Rock",
            "Comedy",
            "Cult",
            "Gangsta",
            "Top 40",
            "Christian Rap",
            "Pop/Funk",
            "Jungle",
            "Native American",
            "Cabaret",
            "New Wave",
            "Psychadelic",
            "Rave",
            "Showtunes",
            "Trailer",
            "Lo-Fi",
            "Tribal",
            "Acid Punk",
            "Acid Jazz",
            "Polka",
            "Retro",
            "Musical",
            "Rock & Roll",
            "Hard Rock"
        };
    }
}
