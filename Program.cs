using System; 
using System.IO;  

namespace KalkulatorKalorii
{
    class Program
    {
        
        // max 100 użytkowników bo ograniczenia pamięciowe i prostota implementacji
        static string[] imiona = new string[100];
        static char[] płeć = new char[100];

        // dane[i, j] - tablica dwuwymiarowa przechowująca dane użytkowników
        // dane[i, 0] = wiek
        // dane[i, 1] = wzrost
        // dane[i, 2] = waga
        // dane[i, 3] = BMI
        // dane[i, 4] = BMR
        static double[,] dane = new double[100, 5]; 
        
        static int liczbaUzytkownikow = 0;

        static void Main(string[] args)
        {
            // Zanim program wogule zaccznie działać odczytuje dane zapisane w pliku poprzednio, jeśli plik istnieje. Jeśli nie istnieje, program zaczyna z pustą listą użytkowników. Jeśli tego by nie było program za każdym razem zaczynał się pustą listą użytkowników i wszystkie dane byłyby tracone po zamknięciu programu.
            OdczytajPlik();
            
            Menu();
            // Zapisanie danych do pliku po zakończeniu programu, aby nie stracić danych użytkowników
            ZapiszDoPliku();
        }

        static void Menu() 
        {
            int wybor;

            do
            {
                Console.Clear(); 

                Console.WriteLine("===== KALKULATOR KALORII =====");
                Console.WriteLine("----  Menu Główne  ----");
                Console.WriteLine("Wybierz opcję:");
                Console.WriteLine("1. Dodaj użytkownika");
                Console.WriteLine("2. Wybierz istniejącego użytkownika");
                Console.WriteLine("3. Wyświetl wszystkich zapisanych użytkowników");
                Console.WriteLine("4. Wyświetl średnie BMI wszystkich użytkowników");
                Console.WriteLine("5. Zapisz dane");
                Console.WriteLine("6. Wyzeruj użytkowników");
                Console.WriteLine("7. Wyjście");

                Console.Write("\nTwój wybór: ");

                while(!int.TryParse(Console.ReadLine(), out wybor))
                {
                    Console.Write("Przepraszamy ta opcja nie istnieje. Podaj poprawny numer opcji: ");
                }

                switch (wybor)
                {
                    case 1:
                        DodajUzytkownika();
                        break;

                    case 2:
                        WybierzUzytkownika();
                        break;

                    case 3:
                        WyświetlUżytkowników();
                        break;

                    case 4:
                        SrednieBMI();
                        break;

                    case 5:
                        ZapiszDoPliku();
                        break;
                    case 6:
                        WyzerujUżytkowników();
                        break;

                    case 7:
                        Console.WriteLine("Dziękujemy za skorzystanie z kalkulatora!");
                        Console.WriteLine("Zamykanie programu...");
                        Console.ReadKey();
                        break;

                    default:
                        Console.WriteLine("Niepoprawny wybór, wybierz ponownie.");
                        break;
                }

                if (wybor != 7)
                {
                    Console.WriteLine("\nNaciśnij dowolny klawisz aby kontynuować...");
                    Console.ReadKey();
                }

            } while (wybor != 7);
        }

        static void DodajUzytkownika()
        {
            Console.Clear();
            string imie;
            do
            {
                Console.Write("\nPodaj imię użytkownika: ");
                imie = Console.ReadLine() ?? "";
                if (string.IsNullOrWhiteSpace(imie))
                {
                    Console.WriteLine("Imie nie może być puste, podaj imie użytownika.");
                }

            } while (string.IsNullOrWhiteSpace(imie));
            imiona[liczbaUzytkownikow] = imie;
            

            char plec;
            while (true)
            {
                Console.Write("\nPodaj Płeć użytkownika (M/K): ");
                string tekst = (Console.ReadLine() ?? "").ToUpper();
                if (tekst == "M" || tekst == "K")
                {
                    plec = tekst[0];
                    break;
                }
                else
                {
                    Console.WriteLine("Niepoprawna wartość. Podaj Płeć użytkownika (M/K): ");
                }
            }
            płeć[liczbaUzytkownikow] = plec;
            Console.Write("\nPodaj wiek użytkownika: "); 
            double wiek;
            while (!double.TryParse(Console.ReadLine(), out wiek) || wiek <= 0)
            {
                Console.Write("Niepoprawna wartość. Podaj wiek użytkownika: ");
            }
            dane[liczbaUzytkownikow, 0] = wiek;
            Console.Write("\nPodaj wzrost użytkownika (cm): ");
            double wzrost;
            while (!double.TryParse(Console.ReadLine(), out wzrost) || wzrost <= 0)
            {
                Console.Write("Niepoprawna wartość. Podaj wzrost użytkownika (cm): ");
            }
            dane[liczbaUzytkownikow, 1] = wzrost;
            Console.Write("\nPodaj wagę użytkownika w pełnych kilogramach: ");
            double waga;
            while (!double.TryParse(Console.ReadLine(), out waga) || waga <= 0)
            {
                Console.Write("Niepoprawna wartość. Podaj wagę użytkownika w pełnych kilogramach: ");
            }
            dane[liczbaUzytkownikow, 2] = waga;
            
            liczbaUzytkownikow++;

            Console.WriteLine("\nUżytkownik został dodany.");
        }

        static void WybierzUzytkownika()
        {
            Console.Clear();
            // co się wyświetli jeśli nie ma zapisanych użytkowników
            if (liczbaUzytkownikow == 0)
            {
                Console.WriteLine("Brak zapisanych użytkowników. Wróć do menu głównego i dodaj użytkownika.");
                Console.WriteLine("\nNaciśnij dowolny klawisz aby kontynuować...");
                Console.ReadKey();
                return;
            }

            // wyświetlenie listy użytkowników i wybór jednego z nich
            for (int i = 0; i < liczbaUzytkownikow; i++)
            {
                Console.WriteLine($"{i + 1}. {imiona[i]}");
            }
            Console.Write($"\nWybierz użytkownika(1 - {liczbaUzytkownikow}): ");

            int wybor;
            while (!int.TryParse(Console.ReadLine(), out wybor))
            {
                Console.Write("Przepraszamy ta opcja nie istnieje. Podaj poprawny numer opcji: ");
            }
            
            wybor--; // zmiana na indeks tablicy (0-based)

            if (wybor >= 0 && wybor < liczbaUzytkownikow)
            // wywołanie metody MenuUzytkownika z przekazaniem numeru wybranego użytkownika sprawdzając czy jest poprawny
            {
                MenuUzytkownika(wybor);
            }
            else
            {
                Console.WriteLine("Niepoprawny wybór.");
            }

        }

        static void MenuUzytkownika(int i)
        { 
            int wybor;

            do
            {
                Console.Clear();
                Console.WriteLine($"===== MENU UŻYTKOWNIKA: {imiona[i]} =====");
                Console.WriteLine("Wybierz opcję:");
                Console.WriteLine("1. Oblicz BMI");
                Console.WriteLine("2. Oblicz BMR");
                Console.WriteLine("3. Pokaż dane użytkownika");
                Console.WriteLine("4. Powrót do menu głównego");
                Console.Write("\nTwój wybór: ");

                while(!int.TryParse(Console.ReadLine(), out wybor))
                {
                    Console.Write("Przepraszamy ta opcja nie istnieje. Podaj poprawny numer opcji: ");
                }

                switch (wybor)
                {
                    case 1:
                        ObliczBMI(i);
                        break;
                    case 2:
                        ObliczBMR(i);
                        break;
                    case 3:
                        PokażDaneUzytkownika(i);
                        break;
                    case 4:
                        Console.WriteLine("Poczekaj chwile, wracamy do menu głównego...");
                        break;
                    default:
                        Console.WriteLine("Niepoprawny wybór, wybierz ponownie.");
                        break;
                }
                if (wybor != 4)
                {
                    Console.WriteLine("\nNaciśnij dowolny klawisz aby kontynuować...");
                    Console.ReadKey();
                }
            } while (wybor != 4);

        }

        static void ObliczBMI(int i)
        {
            // tutaj obliczenia BMI
            double wzrost = dane[i, 1] / 100; // zamiana cm na m
            double waga = dane[i, 2];
            double bmi = waga / (wzrost * wzrost);
            // zapis do dane[i,3]
            dane[i, 3] = bmi;
            Console.WriteLine($"BMI użytkownika wynosi: {bmi:F2}");
            // if sprawdzający kategorię
            if (bmi < 18.5)
            {
                Console.WriteLine("Masz niedowagę, zalecamy skonsultować się z dietetykiem.");
            }
            else if (bmi >= 18.5 && bmi < 24.9)
            {
                Console.WriteLine("Twoja waga jest prawidłowa, nie ma żadnych problemów.");
            }
            else if (bmi >= 25 && bmi < 29.9)
            {
                Console.WriteLine("Masz nadwagę, zalecamy skonsultować się z dietetykiem.");
            }
            else
            {
                Console.WriteLine("Masz otyłość, zalecamy skonsultować się z dietetykiem.");
            }
        }

        static void ObliczBMR(int i)
        {
            double wiek = dane[i, 0];
            double wzrost = dane[i, 1];
            double waga = dane[i, 2];

            double bmr;
            // if dla płci
            if (płeć[i] == 'M')
            {
                bmr = 10 * waga + 6.25 * wzrost - 5 * wiek + 5;
            }
            else
            {
                bmr = 10 * waga + 6.25 * wzrost - 5 * wiek - 161;
            }
            // switch dla aktywności
            Console.WriteLine("\n Wybierz poziom aktywności fizycznej:");
            Console.WriteLine("1. Siedzący tryb życia (brak ćwiczeń)");
            Console.WriteLine("2. Lekka aktywność (1-3 dni w tygodniu)");
            Console.WriteLine("3. Umiarkowana aktywność (3-5 dni w tygodniu)");
            Console.WriteLine("4. Duża aktywność (6-7 dni w tygodniu)");
            Console.WriteLine("5. Bardzo duża aktywność (ciężka praca fizyczna lub trening dwa razy dziennie)");

            int wybor;
            while (!int.TryParse(Console.ReadLine(), out wybor))
            {
                Console.Write("Przepraszamy ta opcja nie istnieje. Podaj poprawny numer opcji: ");
            }

            double pal = 1.2; // współczynnik aktywności fizycznej

            switch (wybor)
            {
                case 1:
                    pal = 1.2;
                    break;
                case 2:
                    pal = 1.375;
                    break;
                case 3:
                    pal = 1.55;
                    break;
                case 4:
                    pal = 1.725;
                    break;
                case 5:
                    pal = 1.9;
                    break;
                default:
                    Console.WriteLine("Niepoprawny wybór, ustawiono domyślny współczynnik aktywności fizycznej (1.2).");
                    break;
            }
            // zapis do dane[i,4]

            double kalorie = bmr * pal;
            dane[i, 4] = kalorie;

            Console.WriteLine($"\nTwoje BMR wynosi: {bmr:F2} kcal na dzień");
            Console.WriteLine($"By utrzymać wagę, potrzebujesz około {kalorie:F2} kcal na dzień.");
            Console.WriteLine($"By schudnąć, zaleca się byś spżywał około {kalorie - 500:F2} kcal na dzień.");
        }

        static void WyświetlUżytkowników()
        {
            // pętla wyświetlająca wszystkich
            Console.Clear();
            Console.WriteLine("Lista zapisanych użytkowników:\n");
            for (int i = 0; i < liczbaUzytkownikow; i++)
            {
                Console.WriteLine($"{i + 1}.\t{imiona[i]}\t{płeć[i]}\t{dane[i, 0]}\t{dane[i, 1]}\t{dane[i, 2]}");
            }
                    

        }

        
        static void SrednieBMI()
        {
            // przypadek gdy nie ma zapisanych użytkowników
            if (liczbaUzytkownikow == 0)
            {
                Console.WriteLine("Brak zapisanych użytkowników. Wróć do menu głównego i dodaj użytkownika.");
                Console.WriteLine("\nNaciśnij dowolny klawisz aby kontynuować...");
                Console.ReadKey();
                return;
            }
            // obliczenie średniej BMI
            double sumaBMI = 0;
            for (int i = 0; i < liczbaUzytkownikow; i++)
            {
                sumaBMI += dane[i, 3];
            }
            double srednieBMI = sumaBMI / liczbaUzytkownikow;
            Console.WriteLine($"\nŚrednie BMI użytkowników wynosi: {srednieBMI:F2}");

        }

                static void ZapiszDoPliku()
        {
            // zapis do pliku
            using (StreamWriter sw = new StreamWriter("dane.txt"))
            {
                sw.WriteLine(liczbaUzytkownikow);
                for (int i = 0; i < liczbaUzytkownikow; i++)
                {
                    sw.WriteLine($"{imiona[i]},{płeć[i]},{dane[i, 0]},{dane[i, 1]},{dane[i, 2]},{dane[i, 3]},{dane[i, 4]}");
                }
            }
        }
        // jesli tego nie po zamknięciu programu dane wprowadzone są tracone 
        static void OdczytajPlik()
        {
            if (File.Exists("dane.txt"))
            {
                using (StreamReader sr = new StreamReader("dane.txt"))
                {
                    liczbaUzytkownikow = int.Parse(sr.ReadLine() ?? "0");
                    for (int i = 0; i < liczbaUzytkownikow; i++)
                    {
                        string[] linia = (sr.ReadLine() ?? "").Split(',');
                        imiona[i] = linia[0];
                        płeć[i] = char.Parse(linia[1]);
                        dane[i, 0] = double.Parse(linia[2]);
                        dane[i, 1] = double.Parse(linia[3]);
                        dane[i, 2] = double.Parse(linia[4]);
                        dane[i, 3] = double.Parse(linia[5]);
                        dane[i, 4] = double.Parse(linia[6]);
                    }
                }
            }
            else
            {
                Console.WriteLine("Brak pliku z danymi. Rozpoczynamy z pustą listą użytkowników.");
            }
        }
        static void PokażDaneUzytkownika(int i)
        {
            Console.Clear();
            Console.WriteLine($"Dane użytkownika: {imiona[i]}");
            Console.WriteLine($"Płeć: {płeć[i]}");
            Console.WriteLine($"Wiek: {dane[i, 0]} lat");
            Console.WriteLine($"Wzrost: {dane[i, 1]} cm");
            Console.WriteLine($"Waga: {dane[i, 2]} kg");
            Console.WriteLine($"BMI: {dane[i, 3]:F2}");
            Console.WriteLine($"BMR: {dane[i, 4]:F2} kcal/dzień");
        }
        static void WyzerujUżytkowników()
        {
            Console.Clear();
            Console.WriteLine("--PRZYSTĘPOWANIE DO WYZEROWANIA UŻYRKOWNIKÓW--");
            Console.WriteLine("Czy jesteś pewnien że chcesz wyzerować wszystkich użytkowników? (TAK/NIE)");
            string odpowiedz = (Console.ReadLine() ?? "").Trim().ToUpper();
            if ( odpowiedz == "TAK")
            {
                liczbaUzytkownikow = 0;
                if (File.Exists("dane.txt"))
                {
                    File.Delete("dane.txt");
                }
                Console.WriteLine("Wszyscy użytkownicy zostali wyzerowani.");
            }
            else
            {
                Console.WriteLine("Wyzerowanie użytkowników zostało anulowane.");
            }
        }

    }
}


