using E_Commerce_Project.DAL.ORM.Context;
using E_Commerce_Project.DAL.ORM.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Project.DAL.ORM.Tools
{
    public class DataInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            DataContext db = new DataContext();

            List<Shipping> shippings = new List<Shipping>()
            {
                new Shipping(){ Name="Aras Kargo"},
                new Shipping(){ Name="Yurtiçi Kargo"},
                new Shipping(){ Name="MNG Kargo"},
                new Shipping(){ Name="UPS Kargo"},
            };

            foreach(var shipping in shippings)
            {
                db.Shippings.Add(shipping);
            }

            db.SaveChanges();


            List<OrderStatus> orderStatuses = new List<OrderStatus>()
            {
                new OrderStatus()
                {
                    Status="Onay Bekleniyor",
                    Description="Siparişiniz onay işlemi sonrasında kargoya verilecektir."
                },
                new OrderStatus()
                {
                    Status="Kargoya Verildi",
                    Description="Siparişiniz kargoya verilmiştir, kargo takip numaranız ile siparişinizi takip edebilirsiniz."
                },
                new OrderStatus()
                {
                    Status="İptal Edildi",
                    Description="Siparişi iptal ettiğiniz için üzgünüz. Olumsuz görüşleriniz için lütfen bizimle iletişime geçiniz."
                },
                new OrderStatus()
                {
                    Status="Ürün Temin Edilemiyor",
                    Description="Siparişleriniz arasında bulunan üründen veya ürünlerden stoklarımızda yeterince bulunmadığından siparişiniz onaylayamıyoruz. Lütfen siparişinizdeki ürün adetlerinizi stok durumuna göre tekrar gözden geçiriniz."
                },
            };

            foreach(var orderStatus in orderStatuses)
            {
                db.OrderStatuses.Add(orderStatus);
            }

            db.SaveChanges();

            List<MainCategory> mainCategories = new List<MainCategory>()
            {
                new MainCategory()
                {
                    Id="00000000-3e66-4dba-99e3-d255f90080cd",
                    Name="Bilgisayar",
                    Description="Gündelik yaşamda, iş yerinde ve okul projelerinizde kullanmak için ihtiyaç duyduğunuz her renk, marka ve modeldeki masaüstü bilgisayar, netbook ve tabletler bilgisayar kategorisinde yer almaktadır.",
                },
                new MainCategory()
                {
                    Id="00000001-d8fe-412a-b288-7282cbc4106e",
                    Name="Cep Telefonu",
                    Description="Cep telefonu, kolayca taşınabilen, geniş kapsama alanlı, kablosuz telefon sistemini kullanan bir iletişim ve multimedya aygıtı. Cep telefonu ile sağlanan hizmetler, telefon modeline ve servis sağlayıcıya göre değişmekle beraber en yaygın olarak kullanılanları, sesli görüşme ve kısa mesaj hizmetidir.",
                },
                new MainCategory()
                {
                    Id="00000002-f3b0-4391-8b27-ebd917c0e448",
                    Name="Tablet",
                    Description="Tablet bilgisayar veya sadece tablet, tek bir ünitede ekran devresi ve batarya bulunan bir mobil bilgisayardır. Tabletler parmak-kalem hareketlerini algılayabilen, fare ve klavye ile kombine edilebilen, kamera ve ivmeölçer gibi sensörlerle donatılan cihazlardır.",
                },
                new MainCategory()
                {
                    Id="00000003-5718-4d39-a9d4-8611e840c6fe",
                    Name="Televizyon",
                    Description="Televizyon Portali. Televizyon veya kısaca TV, bir vericiden elektromanyetik dalga hâlinde yayınlanan görüntü ve seslerin, ekranlı ve hoparlörlü elektronik alıcılar sayesinde yeniden görüntü ve sese çevrilmesini sağlayan haberleşme sistemidir.",
                },
                new MainCategory()
                {
                    Id="00000004-b314-4469-9eba-671c792acdac",
                    Name="Beyaz Eşya",
                    Description="Beyaz eşya, ilk zamanlarda dış görünümünde genellikle beyaz renk kullanılmış olan, fabrikalarda üretilen, ev eşyası makinelerdir. Bilimsel araştırmalara göre bir toplumun kalkınma seviyesinin bir göstergesidir.",
                },
                new MainCategory()
                {
                    Id="00000005-aef7-4de7-8aca-ff2f55d6823f",
                    Name="Küçük Ev Aletleri",
                    Description="İngilizceden çevrilmiştir-Küçük bir ev aleti, küçük ev aleti ya da küçük elektrik aletleri, ev işlerini yapmak için genellikle masa üstleri, tezgah üstü ya da diğer platformlarda kullanılan taşınabilir ya da yarı taşınabilir makinelerdir.",
                },
                new MainCategory()
                {
                    Id="00000006-4f8c-4753-8e67-f95aa72da7c4",
                    Name="Mobilya",
                    Description="Mobilya, oturulan yerlerin süslenmesine ve türlü amaçlarla donatılmasına yarayan eşya. Mobilya denilince ilk akla gelen ahşap mobilyadır.",
                },
                new MainCategory()
                {
                    Name="Deneme",
                    Description="Deneme",
                },

            };

            foreach(var mainCategory in mainCategories)
            {
                db.MainCategories.Add(mainCategory);
            }

            db.SaveChanges();

            List<SubCategory> subCategories = new List<SubCategory>()
            {
                new SubCategory()
                {
                    Name="Ultrabook",
                    Description="Ultrabook, geliştirilmesine 2011 yılında başlanan ve tescili Intel firmasına ait olan ince, hafif, yüksek performanslı, güvenli ve kolay taşınabilir dizüstü bilgisayar konseptine verilen isimdir.",
                    MainCategoryId=mainCategories.Where(i=>i.Name=="Bilgisayar").FirstOrDefault().Id
                },
                 new SubCategory()
                {
                    Name="Laptop",
                    Description="Dizüstü bilgisayar ya da laptop, taşınabilir türden, genellikle ekran ve klavye olmak üzere iki parçadan oluşan kişisel bilgisayarlardır. Bir dizüstü bilgisayar bir masaüstü bilgisayarın klavye, fare ve ekran gibi bileşenlerini tek bir parçada toplar.",
                    MainCategoryId=mainCategories.Where(i=>i.Name=="Bilgisayar").FirstOrDefault().Id
                },
                 new SubCategory()
                {
                    Name="Masaüstü",
                    Description="Masaüstü bilgisayar Sabit bir konsol veya masa üzerine uygun yapıda tasarlanan kişisel bilgisayar türüdür. Bu bilgisayarlar çeşitli türlerde parçaların birleştirilmesiyle çok farklı biçimde oluşturulabilirler. Boyutları büyük ve ağır olması sebebiyle çevresel faktörlerden daha az zarar görürler.",
                    MainCategoryId=mainCategories.Where(i=>i.Name=="Bilgisayar").FirstOrDefault().Id
                },
                 new SubCategory()
                {
                    Name="All In One",
                    Description="All-in-One PC ler (Hepsi bir arada masaüstleri) olarak da bilinen hepsi bir arada bilgisayarlar, tüm PC'nin tek bir ünitede yer alması için bilgisayar kasası ve sistem bileşenlerini monitöre entegre eder. All-in-one (AIO) masaüstü bilgisayarları, masaüstü PC'lerden daha küçük bir form faktörü avantajı sunar, ancak daha yüksek maliyet, daha zayıf performans ve sınırlı yükseltme seçenekleri de dahil olmak üzere, çoğu zaman çeşitli dezavantajları da beraberinde getirir.",
                    MainCategory=mainCategories.Where(i=>i.Name=="Bilgisayar").FirstOrDefault()
                }
            };

            foreach(var subCategory in subCategories)
            {
                db.SubCategories.Add(subCategory);
            }

            db.SaveChanges();

            List<Product> products = new List<Product>()
            {
                new Product()
                {
                    Name = "Huawei Matebook 13\" Intel® Core i5-8265U 8GB 256GB Gri Notebook",
                    Code="73072",
                    Price=5999m,
                    Description="Görmek İnanmaktır\nHUAWEI FullView Display etkileyici bir ekrandan daha fazlasını sunar; tam anlamıyla sürükleyici bir görsel deneyimi hayata getiriyor. % 88 ekran - gövde oranı ile HUAWEI MateBook 13’ün ince 4.4 mm çerçevesi, hayal gücünüzü ateşlemek için parlak, zengin ve canlı grafiklere daha fazla alan yaratır.Büyülenmeye hazır olun.\n\n\nTüm Varlığıyla\nHafif ancak güçlü olan HUAWEI MateBook 13, optimum taşınabilirlik için tasarlandı. 14.9 mm metalik ince çerçevesi, ultra modern premium bir görünüm ve his sağlayan her bir köşesinde elmas kesim ile titizlikle hazırlanmış.Güçlü ve zarif bir makine.\n\n\nOldukça Hızlı\nGünlük yaşamınızı kolaylaştırın ve yaratıcılığınızı hızlandırın. Ultra ince gövdesiyle 2 GB GDDR5 özellikli NVIDIA® GeForce® MX150'ye sahiptir. Görüntü işleme ve video düzenleme* için entegre grafik kartlarına göre 4 kata kadar daha fazla performans elde edin*. Grafik kartı aynı zamanda video düzenleme ve oyun oynamayı sorunsuz, hızlı ve güvenilir hale getirmek için 25 W TDP ile desteklenir.\n\n\nÇığır Açan Güç\nHUAWEI MateBook 13'te hız ve güç bir araya gelerek devrim niteliğinde bir performans sunuyor. Yeni 8. Nesil (Whiskey Lake) Intel® core™ i7 8565U işlemcisiyle*, öncekinden %40 daha hızlı, KBL-R işlemcisinden %10 daha hızlı çalışıyor. Güç ve uzun ömürlülük açısından, dizüstü bilgisayar teknolojisi için ezber bozuyor.\n\n\nDaha Çok Keyfini Çıkar\n42 Wh(TYP) sınıfının en iyi bataryasıyla, sizi daha uzun süre güçlendiriyor. Entegre grafik kartına sahip HUAWEI MateBook 13, 10 saat boyunca, ayrık grafik kartına sahip HUAWEI MateBook 13, 9.6 saat boyunca yerel 1080P videoları oynatabilir *.\n\n\nHızlı Isı Dağılımı\nHUAWEI’nin yeni Shark Fin 2.0 fan tasarımı, çift fan kullanarak ısıyı hiç olmadığı kadar hızlı dağıtabilir. Normal fanların % 25 hız artışı*, daha iyi bir çalışma deneyimi için bilgisayarı hızlı ve sessiz bir şekilde soğutmak adına hava akışını artırır.Bu yeni teknoloji, birden fazla program çalıştırırken veya yoğun oyunlar oynarken bile iş akışınızın kesintisiz ve sorunsuz olacağı anlamına gelir.\n\n\nOneHop ile Akıllı Paylaşma\nTek bir dokunuşla telefondan PC'ye* NFC üzerinden video ve fotoğraf aktarın. 1 dakikada 500 fotoğraf ** ve 35 saniyede 1 GB video yükleyin***. Resimleriniz metin içeriyorsa, HUAWEI MateBook 13 resimdeki kelimeleri kolay düzenleme için çıkarabilir.\n\n\nHazır Ol, Bas, Başla\nHUAWEI MateBook 13’ün optimize edilmiş BIOS’a sahip tek dokunuşlu güç düğmesi hızlı bir şekilde başlatmanızı ve giriş yapmanızı sağlar. Güvenli, hızlı ve kullanıcı dostu.\n\n\nHızlı ve Anında Şarj\nHUAWEI MateBook 13, şarj cihazından çok daha fazlası olan bir cep şarj cihazına sahiptir.Çok fonksiyonlu konektörlerle ve QuickCharge özelliğiyle dizüstü bilgisayarınızı ve telefonunuzu hızlıca şarj edebilir.Nerede olursanız olun 15 dakikalık bir şarjla 2.5 saat boyunca ofis kullanımı sağlar.Hızlı, çevre dostu ve taşınabilir; cebinize de sığar hayatınıza da sığar.\n\n\nSadece Duyma, Hisset\nHUAWEI MateBook 13, Dolby Atmos®*ile etkileyici bir sese sahiptir ve nefes kesen, etrafınızda hareket eden seslerle inanılmaz bir kulaklık deneyimi yaratır.Ses zenginliği ve derinliği sayesinde sesler hayat bulurken ve üç boyutlu bir alanda hareket ediyor gibi, kendinizi aksiyonun içinde hissedeceksiniz.\n\n\nGöz Kolaylığı\nHUAWEI MateBook 13’ün son teknoloji ekranı, zaman içinde gözleri zorlayan mavi ışığı % 30'a kadar filtreleyebilir. Retinanızı korumak ve göz yorgunluğunu önlemek için beyaz dengesini ve renk tonunu otomatik olarak ayarlamak üzere Göz Konforu Modunu seçmeniz yeterlidir; istediğiniz süre boyunca en iyi konforda çalışmanıza ve oynamanıza imkan verir.",
                    Stock =25,
                    DiscountPercentage=10,
                    Brand="Huawei",
                    Model="Matebook 13",
                    SubCategoryId =db.SubCategories.Where(i=>i.Name=="Ultrabook").FirstOrDefault().Id,
                    InSales=true,
                    DiscountDescription="Şok!!!! hiç bir yerde bu fiyatı bulamazsınız. ALDIN ALDIN",
                    Images= new List<Image>()
                    {
                        new Image(){Paht="/Content/image/product/huawei_matebook13/1.jpg"},
                        new Image(){Paht="/Content/image/product/huawei_matebook13/2.jpg"},
                        new Image(){Paht="/Content/image/product/huawei_matebook13/3.jpg"}
                    }
                },
                new Product()
                {
                    Name = "HP 15-BS154NT Intel Core i3-5005U 4GB 128GB SSD Windows 10 Home 15.6\" Taşınabilir Bilgisayar 4UL32EA",
                    Code="00000",
                    Price=2399m,
                    Description="Hızlı Isı Dağılımı\nHUAWEI’nin yeni Shark Fin 2.0 fan tasarımı, çift fan kullanarak ısıyı hiç olmadığı kadar hızlı dağıtabilir. Normal fanların % 25 hız artışı*, daha iyi bir çalışma deneyimi için bilgisayarı hızlı ve sessiz bir şekilde soğutmak adına hava akışını artırır.Bu yeni teknoloji, birden fazla program çalıştırırken veya yoğun oyunlar oynarken bile iş akışınızın kesintisiz ve sorunsuz olacağı anlamına gelir.\n\n\nOneHop ile Akıllı Paylaşma\nTek bir dokunuşla telefondan PC'ye* NFC üzerinden video ve fotoğraf aktarın. 1 dakikada 500 fotoğraf ** ve 35 saniyede 1 GB video yükleyin***. Resimleriniz metin içeriyorsa, HUAWEI MateBook 13 resimdeki kelimeleri kolay düzenleme için çıkarabilir.\n\n\nHazır Ol, Bas, Başla\nHUAWEI MateBook 13’ün optimize edilmiş BIOS’a sahip tek dokunuşlu güç düğmesi hızlı bir şekilde başlatmanızı ve giriş yapmanızı sağlar. Güvenli, hızlı ve kullanıcı dostu.\n\n\nHızlı ve Anında Şarj\nHUAWEI MateBook 13, şarj cihazından çok daha fazlası olan bir cep şarj cihazına sahiptir.Çok fonksiyonlu konektörlerle ve QuickCharge özelliğiyle dizüstü bilgisayarınızı ve telefonunuzu hızlıca şarj edebilir.Nerede olursanız olun 15 dakikalık bir şarjla 2.5 saat boyunca ofis kullanımı sağlar.Hızlı, çevre dostu ve taşınabilir; cebinize de sığar hayatınıza da sığar.\n\n\nSadece Duyma, Hisset\nHUAWEI MateBook 13, Dolby Atmos®*ile etkileyici bir sese sahiptir ve nefes kesen, etrafınızda hareket eden seslerle inanılmaz bir kulaklık deneyimi yaratır.Ses zenginliği ve derinliği sayesinde sesler hayat bulurken ve üç boyutlu bir alanda hareket ediyor gibi, kendinizi aksiyonun içinde hissedeceksiniz.\n\n\nGöz Kolaylığı\nHUAWEI MateBook 13’ün son teknoloji ekranı, zaman içinde gözleri zorlayan mavi ışığı % 30'a kadar filtreleyebilir. Retinanızı korumak ve göz yorgunluğunu önlemek için beyaz dengesini ve renk tonunu otomatik olarak ayarlamak üzere Göz Konforu Modunu seçmeniz yeterlidir; istediğiniz süre boyunca en iyi konforda çalışmanıza ve oynamanıza imkan verir.",
                    Stock =5,
                    DiscountPercentage=0,
                    Brand="Hp",
                    Model="15-BS154NT",
                    SubCategoryId =db.SubCategories.Where(i=>i.Name=="Laptop").FirstOrDefault().Id,
                    InSales=true,
                    Images= new List<Image>()
                    {
                        new Image(){Paht="/Content/image/product/hp_15-BS154NT/4.jpg"},
                        new Image(){Paht="/Content/image/product/hp_15-BS154NT/5.jpg"},
                        new Image(){Paht="/Content/image/product/hp_15-BS154NT/6.jpg"}
                    }
                },
                new Product()
                {
                    Name = "Ürün1 All In One",
                    Code="11111",
                    Price=1000m,
                    Description="Açıklama1",
                    SubCategoryId =db.SubCategories.Where(i=>i.Name=="All In One").FirstOrDefault().Id,
                },
                new Product()
                {
                    Name = "Ürün2 All In One",
                    Code="22222",
                    Price=2000m,
                    Description="Açıklama2",
                    SubCategoryId =db.SubCategories.Where(i=>i.Name=="All In One").FirstOrDefault().Id,
                },
                new Product()
                {
                    Name = "Ürün3 All In One",
                    Code="33333",
                    Price=3000m,
                    Description="Açıklama3",
                    SubCategoryId =db.SubCategories.Where(i=>i.Name=="All In One").FirstOrDefault().Id,
                },
                new Product()
                {
                    Name = "Ürün1 Laptop",
                    Code="11111",
                    Price=1000m,
                    Description="Açıklama1",
                    SubCategoryId =db.SubCategories.Where(i=>i.Name=="Laptop").FirstOrDefault().Id,
                },
                new Product()
                {
                    Name = "Ürün2 Laptop",
                    Code="22222",
                    Price=2000m,
                    Description="Açıklama2",
                    SubCategoryId =db.SubCategories.Where(i=>i.Name=="Laptop").FirstOrDefault().Id,
                },
                new Product()
                {
                    Name = "Ürün3 Laptop",
                    Code="33333",
                    Price=3000m,
                    Description="Açıklama3",
                    SubCategoryId =db.SubCategories.Where(i=>i.Name=="Laptop").FirstOrDefault().Id,
                },
                new Product()
                {
                    Name = "Ürün4 Laptop",
                    Code="44444",
                    Price=4000m,
                    Description="Açıklama4",
                    SubCategoryId =db.SubCategories.Where(i=>i.Name=="Laptop").FirstOrDefault().Id,
                },
                new Product()
                {
                    Name = "Ürün5 Laptop",
                    Code="55555",
                    Price=5000m,
                    Description="Açıklama5",
                    SubCategoryId =db.SubCategories.Where(i=>i.Name=="Laptop").FirstOrDefault().Id,
                },
                new Product()
                {
                    Name = "Ürün1 Masaüstü",
                    Code="11111",
                    Price=1000m,
                    Description="Açıklama1",
                    SubCategoryId =db.SubCategories.Where(i=>i.Name=="Masaüstü").FirstOrDefault().Id,
                },
                new Product()
                {
                    Name = "Ürün2 Masaüstü",
                    Code="22222",
                    Price=2000m,
                    Description="Açıklama2",
                    SubCategoryId =db.SubCategories.Where(i=>i.Name=="Masaüstü").FirstOrDefault().Id,
                },
                new Product()
                {
                    Name = "Ürün1 Ultrabook",
                    Code="11111",
                    Price=1000m,
                    Description="Açıklama1",
                    SubCategoryId =db.SubCategories.Where(i=>i.Name=="Ultrabook").FirstOrDefault().Id,
                },
            };

            foreach(var product in products)
            {
                db.Products.Add(product);
            }

            db.SaveChanges();
        }
    }
}
