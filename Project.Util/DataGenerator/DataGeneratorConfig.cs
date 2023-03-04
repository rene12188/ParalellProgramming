using Project.DataGenerator;

namespace Project.Util.DataGenerator;

public class DataGeneratorConfig
{
    public static void CreateConfig(ImportCommunityFactory importCommunityFactory)
    {
        //Quelle https://de.wikipedia.org/wiki/Liste_der_politischen_Bezirke_der_Steiermark#/media/Datei:Karte_Aut_Stmk_Bezirke.png
        importCommunityFactory.AddCommunity("Bruck-Mürzzuschlag");
        importCommunityFactory.AddCommunity("Deutschlandsberg");
        importCommunityFactory.AddCommunity("Graz");
        importCommunityFactory.AddCommunity("Graz-Umgebung");
        importCommunityFactory.AddCommunity("Hartberg-Fürstenfel");
        importCommunityFactory.AddCommunity("Leibnitz");
        importCommunityFactory.AddCommunity("Leoben");
        importCommunityFactory.AddCommunity("Liezen");
        importCommunityFactory.AddCommunity("Murau");
        importCommunityFactory.AddCommunity("Murtal");
        importCommunityFactory.AddCommunity("Südoststeiermark");
        importCommunityFactory.AddCommunity("Voitsberg");
        importCommunityFactory.AddCommunity("Weiz");
        importCommunityFactory.AddCommunity("Externam");

        importCommunityFactory.AddEdge("Graz", "Graz-Umgebung", 0.1);

        importCommunityFactory.AddEdge("Liezen", "Murau", 0.1);
        importCommunityFactory.AddEdge("Liezen", "Murtal", 0.1);
        importCommunityFactory.AddEdge("Liezen", "Leoben", 0.1);
        importCommunityFactory.AddEdge("Liezen", "Bruck-Mürzzuschlag", 0.1);

        importCommunityFactory.AddEdge("Murau", "Murtal", 0.1);

        importCommunityFactory.AddEdge("Murtal", "Leoben", 0.1);
        importCommunityFactory.AddEdge("Murtal", "Voitsberg", 0.1);
        importCommunityFactory.AddEdge("Murtal", "Graz-Umgebung", 0.1);


        importCommunityFactory.AddEdge("Leoben", "Graz-Umgebung", 0.1);
        importCommunityFactory.AddEdge("Leoben", "Bruck-Mürzzuschlag", 0.1);

        importCommunityFactory.AddEdge("Bruck-Mürzzuschlag", "Graz-Umgebung", 0.1);
        importCommunityFactory.AddEdge("Bruck-Mürzzuschlag", "Weiz", 0.1);

        importCommunityFactory.AddEdge("Hartberg-Fürstenfel", "Südoststeiermark", 0.1);

        importCommunityFactory.AddEdge("Voitsberg", "Deutschlandsberg", 0.1);
        importCommunityFactory.AddEdge("Voitsberg", "Graz-Umgebung", 0.1);

        importCommunityFactory.AddEdge("Deutschlandsberg", "Graz-Umgebung", 0.1);
        importCommunityFactory.AddEdge("Deutschlandsberg", "Leibnitz", 0.1);

        importCommunityFactory.AddEdge("Leibnitz", "Südoststeiermark", 0.1);
        importCommunityFactory.AddEdge("Leibnitz", "Graz-Umgebung", 0.1);

        importCommunityFactory.AddEdge("Weiz", "Südoststeiermark", 0.1);
        importCommunityFactory.AddEdge("Weiz", "Hartberg-Fürstenfel", 0.1);
        importCommunityFactory.AddEdge("Weiz", "Graz-Umgebung", 0.1);

        importCommunityFactory.AddEdge("Südoststeiermark", "Graz-Umgebung", 0.1);
        importCommunityFactory.AddEdge("Südoststeiermark", "External", 50);
    }
}