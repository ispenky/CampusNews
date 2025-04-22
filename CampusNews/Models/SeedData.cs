using CampusNews.Data;
using Microsoft.EntityFrameworkCore;

namespace CampusNews.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CampusNewsContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<CampusNewsContext>>()))
            {
                if (context == null || context.Newws == null)
                {
                    throw new ArgumentNullException("Null RazorPagesMovieContext");
                }

                // Look for any movies.
                if (context.Newws.Any())
                {
                    return;   // DB has been seeded
                }

                context.Newws.AddRange(
                    new Newws
                    {
                        Title = "When Harry Met Sally",
                        ReleaseDate = DateTime.Parse("1989-2-12"),
                        Genre = "Romantic Comedy",
                        Price = 7.99M,
                        NewsDetails= "莎莉（梅格·瑞恩 饰）开车载哈利（比利·克里斯托 饰）前往纽约，两人就此相识。途中二人言语相左，不欢而散，并留下经典的问题：“排除‘性’的介入，男人和女人可以成为真正的朋友么？”\r\n　　五年后。两人在机场不期而遇。此时，莎莉沉浸于新恋情的甜蜜，而哈利即将步入正式的婚姻生活。哈利告诉莎莉：“在两人各有所爱的时候，男人和女人或许可以成为朋友，但很快他们会质疑为什么要与只能做朋友的人交往。”莎莉佛袖而去。\r\n　　十年后。失恋中的莎莉再次偶遇刚刚离婚的哈利。哈利对莎莉说：“我们要成为朋友了么？你或许是我不想带上床的第一位迷人女性。”\r\n　　临睡前的通话，共度圣诞，为彼此的感情出谋划策，哈利与莎莉有着不可言喻的默契。然而，这真的是一段美丽友谊的开始么？又或者这悄然来临的是错认了的感情呢…"
                    },

                    new Newws
                    {
                        Title = "Ghostbusters ",
                        ReleaseDate = DateTime.Parse("1984-3-13"),
                        Genre = "Comedy",
                        Price = 8.99M,
                        NewsDetails= "三位大学教授皮特（比尔·默瑞 饰）、雷蒙德（丹·艾克罗伊德 饰）、埃贡（哈罗德·雷米斯 饰）专门研究鬼怪灵异之事，他们决定离开校园，自组一个“捉鬼大队”，以最新科学仪器对付在纽约市出没的猛鬼。他们的捉鬼行动相当成功，数以万计的大鬼小鬼再也无容身之地，但是，政府单位却将他们视为江湖骗术。直到有一天，一群恶鬼包围了纽约市，并附身在美女达娜（西格妮·韦弗 饰）身上，准备在纽约市大举肆虐。此时，无计可施的政府只好召来了捉鬼大队，且看这群“魔鬼克星”是如何让鬼魂无所遁形"
                    },

                    new Newws
                    {
                        Title = "Ghostbusters 2",
                        ReleaseDate = DateTime.Parse("1986-2-23"),
                        Genre = "Comedy",
                        Price = 9.99M,
                        NewsDetails= "捉鬼敢死队员们又一次拿起武器在曼哈顿与恶魔决斗，在纽约城区进行了一场小战斗后，敢死队发现一个千年恶魔准备重返地球。这个恶魔对曼哈顿不怀好意，他隐藏在曼哈顿艺术博物馆的画像里，以此为根据地进行破坏行动，现在只有捉鬼敢死队为了拯救纽约城，他们必须与恶魔进行一场对决"
                    },

                    new Newws
                    {
                        Title = "Rio Bravo",
                        ReleaseDate = DateTime.Parse("1959-4-15"),
                        Genre = "Western",
                        Price = 3.99M,
                        NewsDetails = "德克萨斯的一个镇上，警长John T Chance（约翰·韦恩 John Wayne饰）和副警长酒鬼Dude（迪恩·马丁 Dean Martin饰）将作恶多端的Joe Burdette绳之以法，并将其关在监狱里，和另一个警员瘸腿的Stumpy （沃尔特·布伦南 Walter Brennan 饰）一起看守，等待几天后审讯官的到来。此时，Chance的老友Pat Wheeler正好路经此地，并介绍Chance认识了一个叫Colorado Ryan（瑞奇·尼尔森 Ricky Nelson 饰）的男孩。而Chance也在一个酒店里认识了一个叫Feather（安吉·迪金森 Angie Dickinson 饰）的女人。Joe的哥哥Nathan带着人要来劫狱，先是派人杀死了Pat，又多次想要以Dude来威胁Chance放人。Chance等人与其斗智斗勇，在这个过程中，Chance发现了Colorado的勇敢并将其招入警队；而酒鬼Dude又要如何与自己的酗酒对抗，以争回自己的尊严，Chance能否带着一个酒鬼、一个瘸子和一个男孩来战胜Nathan的一次次进逼？"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
