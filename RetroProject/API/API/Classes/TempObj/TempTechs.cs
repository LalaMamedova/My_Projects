namespace API.Classes.TempObj
{
    public class TempTechs
    {
        public List<Technology> Technologies = new List<Technology>
        {
            new Technology
            {id = 1,
            name = "Commodore 64",
            year = 1960,
            description = "Commodore 64, также известный как C64, был одним из самых популярных домашних компьютеров 1980-х годов. Он имел 8-битный процессор MOS " +
            "Technology 6510, 64 КБ оперативной памяти и характерный SID звуковой чип, который делал его популярным среди музыкантов.",
            type = "Computer",
            images = new List<string> { @"https://spectrum.ieee.org/media-library/image.jpg?id=29576456", "https://assets1.ignimgs.com/2020/01/14/thec64-thumb-1578962621460_160w.jpg?width=1280" },
            charname = new List<string> { "Год релиза" },
            charvalue = new List<string> { "1982" },
            interestingfacts = new List<string> { "Одна из самых продаваемых моделей" }
            },

            new Technology
            {id = 2,
            name = "Atari 2600",
            year = 1970,
            description = "Atari 2600, также известный как Atari VCS (Video Computer System), был первой игровой консолью, которая дала возможность играть в разнообразные видеоигры дома. Он был популярен в 1970-х и начале 1980-х годов.",
            type = "Consoles",
            images = new List<string> { "https://upload.wikimedia.org/wikipedia/commons/b/b9/Atari-2600-Wood-4Sw-Set.jpg" },
            charname = new List<string> { "Год релиза" },
            charvalue = new List<string> { "1977" },
            interestingfacts = new List<string> { "Первая игровая консоль с поддержкой сменных картриджей" }
            },

            new Technology
            {id = 3,
            name= "Floppy Disk",
            description= "A data storage medium that was widely used in the past. Floppy disks store digital data which can be read and written when the disk is inserted into a floppy disk drive (FDD) connected to or inside a computer or other device.Floppy disks became commonplace during the 1980s and 1990s in their use with personal computers to distribute software, transfer data, and create backups. Before hard disks became affordable to the general population,{nb 1} floppy disks were often used to store a computer's operating system (OS). Most home computers from that time have an elementary OS and BASIC stored in read-only memory (ROM), with the option of loading a more advanced OS from a floppy disk.",
            year = 1971,
            type= "Other",
            images= new List<string> {@"https://cdn.retrostylemedia.co.uk/media/img_609bee39f1b57.jpg", @"https://cdn.pixabay.com/photo/2013/07/13/12/52/disk-160525_1280.png", @"https://media.istockphoto.com/id/503180457/tr/foto%C4%9Fraf/hand-inserting-old-floppy-disk-drive-into-vintage-eigthies-computer.jpg?s=612x612&w=0&k=20&c=hq1LCiVWUopi3l0YIjPSgPpmPf9QEkzdmyXCBZfPMhc="},
            interestingfacts=  new List<string> {
                "The first floppy disks had a storage capacity of only 80 kilobytes.",
                "Floppy disks were often used to distribute software in the 1980s and 1990s."
            },
            charname=  new List<string> {"Storage Capacity", "Year of Invention"},
            charvalue= new List<string> {"80 KB","1971"}
            },

            new Technology
            {id = 4,
            name= "CRT Monitor",
            description= "A type of computer monitor that used cathode-ray tube technology.",
            year=1922,
            type= "Other",
            images=new List<string>  { @"https://www.online-tech-tips.com/wp-content/uploads/2019/09/cropped-crt-monitor.jpeg", @"https://www.slashgear.com/img/gallery/10-best-uses-for-old-crt-monitors/l-intro-1652987930.jpg"},
            interestingfacts= new List<string> {"CRT monitors were commonly used as computer displays before the advent of LCD monitors."},
            charname=new List<string>  {"Screen Size","Year of Invention"},
            charvalue= new List<string> {"17 inches", "1971" }
            },

            new Technology
            {id = 5,
            name= "Vinyl Record",
            description= "An analog audio storage medium that plays music using a stylus.",
            year=1877,
            type= "Other",
            images= new List<string> { "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/12in-Vinyl-LP-Record-Angle.jpg/800px-12in-Vinyl-LP-Record-Angle.jpg", "https://globalnews.ca/wp-content/uploads/2019/05/turntable.jpg?quality=85&strip=all"},
            interestingfacts= new List<string> {
              "Vinyl records were the primary format for music distribution before CDs and digital downloads.",
              "They come in various sizes, including 7-inch, 10-inch, and 12-inch records."},
            charname= new List<string> {"Playback Speed","Year of Invention"},
            charvalue= new List < string > { "33 1/3 RPM", "1877" }
            },

            new Technology
             {id = 6,
             name= "Typewriter",
             description= "A mechanical device used for typing text on paper.",
             year=1868,
             type= "Other",
             images= new List < string > { "https://m.media-amazon.com/images/I/71oU+4RiX9L._AC_UF894,1000_QL80_.jpg" },
             interestingfacts= new List<string> {
               "Typewriters were widely used for office work and professional writing in the past.",
               "They were gradually replaced by computer keyboards in the late 20th century."
             },
             charname=new List<string>  {"Number of Keys", "Year of Invention"},
             charvalue= new List<string> {"QWERTY keyboard layout", "1868" }
            },
            new Technology
            {
               id=7,
               name= "Steam Engine",
               description= "An early heat engine that converted steam into mechanical work",
               year= 1705,
               type= "Other",
               images=new List<string> { "https://t0.gstatic.com/licensed-image?q=tbn:ANd9GcQ4ehMxFq4ZtKdHNPTcN-BOQ4NW25g5bQWE6YvYa7T-ksaPJU90SQaDgMVTBGeeOAbs", "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a8/Steam_lokomobile_2_%28aka%29.jpg/800px-Steam_lokomobile_2_%28aka%29.jpg" },
               interestingfacts =new List<string>  {
                "The steam engine played a pivotal role in the Industrial Revolution.",
                 "It powered trains, ships, and various industrial machinery."
               },
               charname= new List<string>{ "Power Source", "Year of Invention"},
               charvalue = new List<string>{ "Year of Invention", "Late  17th century" },
            },
             new Technology
            {
               id=8,
               name= "IMac G3",
               description= "Первая ревизия iMac G3 включала 15-дюймовый (13,8-дюймовый видимый) ЭЛТ-дисплей, процессор 233 МГц, графику ATI Rage IIc, жесткий диск объемом 4 ГБ, дисковод CD-ROM с загрузкой в лоток, два порта USB, модем 56 Кбит/с, встроенный Ethernet",
               year= 1998,
               type= "Computer",
               images=new List<string> { "https://www.iphones.ru/wp-content/uploads/2012/12/01-iMac-G3.jpeg", "https://upload.wikimedia.org/wikipedia/commons/d/db/IMac_G3_Bondi_Blue%2C_three-quarters_view.png" },
               interestingfacts =new List<string>  {"Создателем был Стив Джобс"},
               charname= new List<string>{ "Processor", "RAM"},
               charvalue = new List<string>{ "IBM PowerPC G3", "128 MB" },
            },
              new Technology
            {
               id=9,
               name= "Sega Mega Drive",
               description= " игровая приставка четвёртого поколения, разработанная и выпускавшаяся компанией Sega. Приставка была выпущена в 1988 году в Японии как Mega Drive, в 1989 году в США как Genesis и в 1990 году в Европе (Virgin Mastertronic), Австралии (Ozisoft) и Бразилии (Tec Toy) — вновь под названием Mega Drive. Причиной изменения названия при запуске приставки на рынке США явилась невозможность официальной регистрации торговой марки Mega Drive. Sega Mega Drive — это третья аппаратная платформа от Sega после Sega Master System. В Южной Корее распространением приставки занималась компания Samsung; приставка имела название Super Gam*Boy, позднее изменённое на Super Aladdin Boy.",
               year= 1988,
               type= "Console",
               images=new List<string> { "https://www.videogameschronicle.com/files/2022/06/mega-drive-mini-2-1.jpg", "https://images-eu.ssl-images-amazon.com/images/G/02/uk-videogames/2014/ConsoleComp/aplus/MD_ConsoleI_lg._V344383332_.jpg" },
               interestingfacts =new List<string>  {
                "Беспеллером в этой консоли был Song the hedgehog",
                 "Всего было продано 39 миллионов штук."
               },
               charname= new List<string>{ "Processor","Additional Processor", "Memory"},
               charvalue = new List<string>{ "Motorola 68000", "Zilog Z80","72 kb" },
            },
        };
    }
}
