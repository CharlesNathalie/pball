namespace pball.Services.Tests;

public partial class BaseServiceTests
{
    public async Task<bool> FillDBWithTestDataAsync(int ContactNumber, int LeagueNumber, int LeagueContactNumber, int GamePerLeagueNumber)
    {
        Random random = new Random();

        List<Contact> contactList = new List<Contact>();
        List<Game> gameList = new List<Game>();
        List<League> leagueList = new List<League>();
        List<LeagueContact> leagueContactList = new List<LeagueContact>();

        ErrRes errRes = new ErrRes();

        // clears db.Games, db.LeagueContacts, db.Leagues, db.Contacts
        if (db != null && db.Games != null && db.LeagueContacts != null && db.Leagues != null && db.Contacts != null)
        {
            List<Game> gameListToDelete = (from c in db.Games
                                           select c).ToList();

            try
            {
                db.Games.RemoveRange(gameListToDelete);
            }
            catch (Exception ex)
            {
                Assert.True(false, ex.Message);
            }

            List<LeagueContact> leagueContactListToDelete = (from c in db.LeagueContacts
                                                             select c).ToList();

            try
            {
                db.LeagueContacts.RemoveRange(leagueContactListToDelete);
            }
            catch (Exception ex)
            {
                Assert.True(false, ex.Message);
            }

            List<League> leagueListToDelete = (from c in db.Leagues
                                               select c).ToList();

            try
            {
                db.Leagues.RemoveRange(leagueListToDelete);
            }
            catch (Exception ex)
            {
                Assert.True(false, ex.Message);
            }

            List<Contact> contactListToDelete = (from c in db.Contacts
                                                 select c).ToList();

            try
            {
                db.Contacts.RemoveRange(contactListToDelete);
            }
            catch (Exception ex)
            {
                Assert.True(false, ex.Message);
            }

        }


        List<string> firstNameList = new List<string>()
        {
            "Rouge", "Red", "Orange", "Violet", "Purple", "Brun", "Brown", "Rose", "Pink", "Bleu", "Blue",
            "Vert", "Green", "BleuCiel", "LightBlue", "BleuMarin", "DarkBlue", "Noir", "Black", "Blanc", "White",
            "Maron", "Turquoise", "RougeVin", "Jaune", "Yellow", "JauneBanane", "Gris", "Gray", "GrisPale", "LightGray",
            "GrisFoncé", "Lumière", "Chaise", "Chair", "Table", "Divan", "Couch", "Silver", "Lime", "Aqua", "Chose"
        };

        List<string> lastNameList = new List<string>()
        {
            "Allain", "Breau", "Boudreau", "Cormier", "Smith", "LeBlanc", "Cormier", "Vautour", "Brown", "Tremblay",
            "Martin", "Roy", "King", "Gagnon", "Lee", "Wilson", "Johnson", "MacDonald", "Taylor", "Campbell", "White",
            "Young", "Bouchard", "Scott", "Stewart", "Pelletier", "Lavoie", "More", "Miller", "Coté", "Bélanger", "Robinson",
            "Landry", "Poirier", "Thomas", "Richard", "Clarke", "Davis", "Evans", "Grant"
        };

        List<string> leagueNameList = new List<string>()
        {
            "Les vendredis", "Moncton non official league of Tuesday",
            "Les Tops de Tops", "Pickleball starters", "All the old cougars",
            "Les insuportables des jeudis matin", "Reds", "Old guys having fun",
            "Let's play for fun", "Les gars de Richibucto", "Moncton East End Youth Center (Monday morning)",
            "Bubblicious", "County Ladies", "The romans",
        };

        List<string> emailList = new List<string>()
        {
            "gmail.com", "yahoo.com", "gnb.ca", "canada.ca", "live.com", "nb.sympatico.ca", "umoncton.ca",
            "hotmail.com", "rogers.com", "nbed.nb.ca", "me.com"
        };

        List<string> initialList = new List<string>
        {
            "A", "B", "C", "D", "E", "F",
            "G", "H", "I", "J", "K", "L",
            "M", "N", "O", "P", "Q", "R",
            "S", "T", "U", "V", "W", "X",
            "Y", "Z",
        };

        int firstNameCount = firstNameList.Count;
        int lastNameCount = lastNameList.Count;
        int leagueNameCount = leagueNameList.Count;
        int emailCount = emailList.Count;
        int initialCount = initialList.Count;

        for (int i = 0; i < ContactNumber; i++)
        {
            bool done = false;
            while (!done)
            {
                string firstName = firstNameList[random.Next(firstNameCount)];
                string lastName = lastNameList[random.Next(lastNameCount)];
                if (!(contactList.Where(c => c.FirstName == firstName && c.LastName == lastName).Any()))
                {
                    contactList.Add(new Contact()
                    {
                        ContactID = i + 1,
                        FirstName = firstNameList[random.Next(firstNameCount)],
                        LastName = lastNameList[random.Next(lastNameCount)],
                        PlayerLevel = (double)(random.Next(1, 4)) + (double)((random.Next(0, 9) / 10.0)),
                    });

                    done = true;
                }
            }
        }

        foreach (Contact contact in contactList)
        {
            bool IsAdmin = random.Next(10) == 5 ? true : false;
            bool initial = random.Next(4) == 2 ? true : false;

            if (ScrambleService != null)
            {
                contact.PasswordHash = ScrambleService.Scramble(contact.LastName);
                contact.LoginEmail = $"{contact.FirstName}.{contact.LastName}@{ emailList[random.Next(0, emailCount)] }";
                contact.IsAdmin = IsAdmin;
                contact.Removed = false;
                contact.LastUpdateContactID = contact.ContactID;
                contact.LastUpdateDate_UTC = DateTime.UtcNow;
                contact.Initial = initial == true ? initialList[random.Next(initialList.Count)] : "";

                if (Configuration != null)
                {
                    byte[] key = Encoding.ASCII.GetBytes(Configuration["APISecret"]);

                    JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                    SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Name, $"{ contact.LoginEmail }")
                        }),
                        Expires = DateTime.UtcNow.AddDays(2),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
                    contact.Token = tokenHandler.WriteToken(token);
                }
            }
        }

        if (db != null && db.Contacts != null)
        {
            try
            {
                db.Contacts.AddRange(contactList);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Assert.True(false, ex.Message);
            }
        }

        for (int i = 0; i < LeagueNumber; i++)
        {
            bool done = false;
            while (!done)
            {
                int ContactID = random.Next(ContactNumber);

                string leagueName = leagueNameList[random.Next(leagueNameCount)];

                if (!(leagueList.Where(c => c.LeagueName == leagueName).Any()))
                {
                    leagueList.Add(new League()
                    {
                        LeagueID = i + 1,
                        LeagueName = leagueNameList[i],
                        PointsToWinners = 3,
                        PointsToLosers = 1,
                        PercentPointsFactor = 0,
                        PlayerLevelFactor = 0,
                        Removed = false,
                        LastUpdateContactID = ContactID,
                        LastUpdateDate_UTC = DateTime.UtcNow,
                    });

                    done = true;
                }
            }
        }

        if (db != null && db.Leagues != null)
        {
            try
            {
                db.Leagues.AddRange(leagueList);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Assert.True(false, ex.Message);
            }
        }

        int leagueContactID = 1;
        foreach (League league in leagueList)
        {
            bool IsLeagueAdmin = true;

            for (int i = 0; i < LeagueContactNumber; i++)
            {
                bool done = false;

                while (!done)
                {
                    int ContactID = contactList[random.Next(ContactNumber)].ContactID;

                    if (!(leagueContactList.Where(c => c.LeagueID == league.LeagueID && c.ContactID == ContactID).Any()))
                    {
                        leagueContactList.Add(new LeagueContact()
                        {
                            LeagueContactID = leagueContactID,
                            ContactID = ContactID,
                            IsLeagueAdmin = IsLeagueAdmin,
                            LeagueID = league.LeagueID,
                            Active = true,
                            PlayingToday = true,
                            Removed = false,
                            LastUpdateContactID = ContactID,
                            LastUpdateDate_UTC = DateTime.UtcNow,
                        });

                        leagueContactID += 1;

                        if (IsLeagueAdmin)
                        {
                            IsLeagueAdmin = false;
                        }

                        done = true;
                    }
                }
            }
        }

        if (db != null && db.LeagueContacts != null)
        {
            try
            {
                db.LeagueContacts.AddRange(leagueContactList);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Assert.True(false, ex.Message);
            }
        }

        if (db != null)
        {
            int gameID = 1;
            foreach (League league in leagueList)
            {
                for (int i = 0; i < GamePerLeagueNumber; i++)
                {
                    List<LeagueContact> leagueContact = (from c in db.LeagueContacts
                                                         where c.LeagueID == league.LeagueID
                                                         select c).ToList();

                    List<int> gameContactIDList = new List<int>();
                    List<double> gameContactLevelList = new List<double>();

                    bool done = false;
                    while (!done)
                    {
                        int ContactID = leagueContact[random.Next(leagueContact.Count)].ContactID;
                        if (!gameContactIDList.Where(c => c == ContactID).Any())
                        {
                            gameContactIDList.Add(ContactID);
                            gameContactLevelList.Add(contactList.Where(c => c.ContactID == ContactID).First().PlayerLevel);
                        }

                        if (gameContactIDList.Count > 3)
                        {
                            done = true;
                        }
                    }

                    double Team1AvgLevel = (gameContactLevelList[0] + gameContactLevelList[1]) / 2.0D;
                    double Team2AvgLevel = (gameContactLevelList[2] + gameContactLevelList[3]) / 2.0D;

                    double Team1ChangeOfWinning = 100 * Team1AvgLevel / (Team1AvgLevel + Team2AvgLevel);

                    bool Team1Won = random.Next(100) > Team1ChangeOfWinning ? true : false;

                    bool To9Point = random.Next(100) > 60 ? true : false;

                    int Score1 = 1;
                    int Score2 = 1;

                    if (Team1Won)
                    {
                        if (To9Point)
                        {
                            Score1 = 9;
                            Score2 = random.Next(7);
                        }
                        else
                        {
                            Score1 = 11;
                            Score2 = random.Next(9);
                        }
                    }
                    else
                    {
                        if (To9Point)
                        {
                            Score2 = 9;
                            Score1 = random.Next(7);
                        }
                        else
                        {
                            Score2 = 11;
                            Score1 = random.Next(9);
                        }
                    }

                    DateTime GameDate = DateTime.Now.AddDays(random.Next(40) * (-1));
                    gameList.Add(new Game()
                    {
                        GameID = gameID,
                        LeagueID = league.LeagueID,
                        Team1Player1 = gameContactIDList[0],
                        Team1Player2 = gameContactIDList[1],
                        Team2Player1 = gameContactIDList[2],
                        Team2Player2 = gameContactIDList[3],
                        GameDate = GameDate,
                        Team1Scores = Score1,
                        Team2Scores = Score2,
                        Removed = false,
                        LastUpdateContactID = gameContactIDList[0],
                        LastUpdateDate_UTC = GameDate,
                    });

                    gameID += 1;
                }
            }
        }

        if (db != null && db.Games != null)
        {
            try
            {
                db.Games.AddRange(gameList);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Assert.True(false, ex.Message);
            }
        }

        return await Task.FromResult(true);
    }
}

