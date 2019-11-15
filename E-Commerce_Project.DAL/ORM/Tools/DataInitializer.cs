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
                db.SaveChanges();
            }

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
                db.SaveChanges();
            }

            List<MainCategory> mainCategories = new List<MainCategory>()
            {
                new MainCategory()
                {
                    Name="Bilgisayar",
                    Description="Gündelik yaşamda, iş yerinde ve okul projelerinizde kullanmak için ihtiyaç duyduğunuz her renk, marka ve modeldeki masaüstü bilgisayar, netbook ve tabletler bilgisayar kategorisinde yer almaktadır.",
                },
            };

            foreach(var mainCategory in mainCategories)
            {
                db.MainCategories.Add(mainCategory);
                db.SaveChanges();
            }

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
                db.SaveChanges();
            }
        }
    }
}
