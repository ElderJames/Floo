using Floo.App.Shared.Cms.Articles;
using Floo.App.Shared.Cms.Comments;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Floo.App.Web.Pages
{
    public partial class Question
    {
        [ParameterAttribute]
        public string username { get; set; }
        [ParameterAttribute]
        public string slug { get; set; }
        private ArticleDto _Question;
        private List<CommentDto> _Comments = new List<CommentDto>();
        private CommentDto _CommentModel;
        private bool inEdit = false;
        private string labelOfEdit = "编辑";
        protected override Task OnInitializedAsync()
        {
            MockDto();
            InitCommentModel();
            return Task.CompletedTask;
        }

        private async Task OnEditButtonClick()
        {
            if (inEdit)
            {
                inEdit = false;
                labelOfEdit = "编辑";
            }
            else
            {
                inEdit = true;
                labelOfEdit = "保存";
            }
        }

        private async Task OnCommitCommentClick()
        {
            _CommentModel.CreatedAtUtc = DateTime.Now;
            _Comments.Add(_CommentModel);
            InitCommentModel();
        }

        private void InitCommentModel()
        {
            _CommentModel = new CommentDto
            {
                Id = 999,
                Avatar = "https://zos.alipayobjects.com/rmsportal/ODTLcjxAfvqbxHnVXCYX.png",
                Author = "Liu"
            };
        }

        private void MockDto()
        {
            _Question = new ArticleDto
            {
                Title = "母猪的产后护理有什么注意点?",
                Contnet = @"母猪意外怀孕生了猪仔,它产后护理有什么注意点吗?",
                Author = "Liu",
                UpdatedAtUtc = DateTime.Now

            };
            for (int i = 0; i < 2; i++)
            {
                _Comments.Add(new CommentDto
                {
                    Id = i,
                    Content = @"拾光小弟

## **前言**

一直老说自己的专业是母猪的产后护理，哎，有人竟然表示质疑，今天就和大家好好聊聊母猪的产后护理，不过这篇文章我估计也没几个人能看完，所以我就开头多和大家聊几句。

![](https://pic1.zhimg.com/80/v2-3e15dea678424a68dec0f9908b2ed580_720w.jpg)

先给大家聊一点关于猪的常识，因为很多人吃过猪肉，但是确实没见过猪跑，猪虽然看似很笨拙，视觉也不好，但是它的嗅觉和听觉很好，嗅觉比狗都好好几倍。人们总说你笨得像头猪，猪真的那么笨嘛，待久了你会发现，你可能还没有它聪明，比你厉害多了。人家会游泳，嗅觉比狗都厉害，而且爱干净，学习能力强。就是懒，好吃好睡。这也是为什么不用猪来做搜救任务吧，就是太笨拙了，当然在搜救猪和食用当中选一个，肯定大多数人还是会选择吃它。

![](https://pic4.zhimg.com/80/v2-11dbf8f1190a07a09a7d1d86b4d68810_720w.jpg)

母猪是双角子宫，有些人很喜欢吃的花肠就是母猪子宫，反正我是吃不来。母猪一般12-14个乳头的居多，这个跟品种有很大关系，所以一般家养母猪都产12头以下，当然规模化养殖都在12头以上了，所以养猪场会配备“奶妈”。母猪怀孕114天左右，人是多少天呢，别告诉我你不知道哈，人是280天左右和牛是差不多的。不过猪场的公猪的命运比较悲惨，出生3-5左右就会被阉割掉，只有极少数公猪能成为真正意义上的公猪。

![](https://picb.zhimg.com/80/v2-95b86933ae1dc397566aaef0d83e163f_720w.jpg)

好了，就闲聊到这儿，接下来我会从母猪的产前到产后如何进行饲喂管理尽可能仔细讲一遍，如果不是相关专业看到这儿就可以走了，后面没有想象的那么有趣。

##
待产母猪的饲喂管理

**1. 喂料**：母猪提前4天上产房，产前3天开始减料，
产前3天饲喂3.5kg/天左右，
产前2天饲喂2.5kg/天左右，
产前1天饲喂1.5kg/天左右，
产仔当天饲喂0.5kg/天左右或不饲喂。
每次投料前清理母猪料槽一次，保证投料前料槽干净，每次投料45-6omin后，清理料槽内剩余的饲料(保证饲料新鲜)，
并投喂其它母猪。每次投完料待母猪吃完后，将掉在地下的饲料及时捡入料槽。待产母猪后期采食量大，易造成母猪便秘的情况，可根据母猪的表现饲喂湿拌料及生物菌液。

![](https://pic4.zhimg.com/80/v2-ae1fccabf659994547ab9f1a32f756fa_720w.jpg)

**2. 温度控制**：舍内温度:母猪上床至产仔当天舍内温度由22度增加至26度，确保母猪逐步适应分娩舍的温度，减少应激环境温度应激。
上产床当天舍内目标温度22℃左右，
上产床第2天舍内目标温度23℃左右，
上产床第3天舍内目标温度24℃左右，
产仔当天舍内目标温度26℃左右。
**3. 饮水**：上产床前要对栏舍的饮水设备进行检修，杜绝因空栏清洗后水线未开和水线消毒后未清理的情况，影响待产母猪的饲喂。测量母猪饮水器的出水量，正常母猪日饮水量15-20L，要求饮水器的每分钟的出水量1.5-2L，夏季我们为了保证母猪有充足的饮水量，可以采用料槽饮水，同时适当提高饮水量。供水管的材质也影响母猪的饮水量。

![](https://picb.zhimg.com/80/v2-589165da01c6741bf788d808e5f2352c_720w.jpg)##

产后母猪的护理

母猪分娩时，生殖器官发生了急剧的变化机体的抵抗力明显下降。因此，母猪产后要进行妥善的护理，让其尽早恢复，投入正常生产。母猪产后要随时观察采食饮水情况及体温变化，注意有无大出血、产后乳房炎、瘫痪、泌乳等情况。对人工助产母猪要清洁产道，并应用药物消
炎。产后2-5d逐渐増加喂料，1周后达最高用量，吃多少给多少。断奶前2-3d，视母猪膘情适当减料。产后护理的主要工作以卫生、饲养、管理、防病为重点。",
                    CreatedAtUtc = DateTime.Now,
                    Avatar = "https://zos.alipayobjects.com/rmsportal/ODTLcjxAfvqbxHnVXCYX.png",
                    Author = "Liu"
                });
            }
        }
    }
}
