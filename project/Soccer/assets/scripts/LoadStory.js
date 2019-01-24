cc.Class({
    extends: cc.Component,

    properties: {
        labelStory:{
            default:null,
            type : cc.Label
        },
        imageStory:{
            default:null,
            type:cc.Sprite
        },
        _storyArray: [],
        get storyArray() {
            return this._storyArray;
        },
        set storyArray(value) {
            this._storyArray = value;
        },
        _storyIndex: 0,
        get storyIndex() {
            return this._storyIndex;
        },
        set storyIndex(value) {
            this._storyIndex = value;
        },
    },

    // LIFE-CYCLE CALLBACKS:

    // onLoad () {},

    start () {
        var self = this;
        this._storyIndex = 0;
        
        //获取剧情
        var xhr = new XMLHttpRequest();        
        xhr.onreadystatechange = function () {
            if (xhr.readyState == 4 && (xhr.status >= 200 && xhr.status < 400)) {
                
                self._storyArray = JSON.parse(xhr.responseText);    
                console.log(xhr.responseText);            
                if(self._storyArray.length === 0)
                {
                    console.log(self._storyArray.length);
                    //return;
                }
                else{
                    console.log(self._storyArray[self._storyIndex].lines);
                    self.labelStory.string = self._storyArray[self._storyIndex].lines;
                    let path = "storyImage/" + self._storyArray[self._storyIndex].scene;
                    cc.loader.loadRes(path, cc.SpriteFrame, function (err, sf) {
                        self.imageStory.spriteFrame = sf;
                    }); 
                    self._storyIndex++;
                }
            }
        };
        let url = "https://rxqq.dingpamao.net/api/story";        
        
        xhr.open("GET", url, true);
        xhr.send();

        //注册touch事件
        this.node.on(cc.Node.EventType.TOUCH_START, this.UpdateStory, this);
    },

    UpdateStory:function(t)
    {
        if(this._storyIndex < this._storyArray.length)
        {
            this.labelStory.string = this._storyArray[this._storyIndex].lines;

            let path = "storyImage/" + this._storyArray[this._storyIndex].scene;
            console.log(path);
            if(!this.imageStory)
            {
                console.log("Not Found imageStory");
                return;
            }
            
            self = this;
            cc.loader.loadRes(path, cc.SpriteFrame, function (err, sf) {
                self.imageStory.spriteFrame = sf;
            });                 

            this._storyIndex++;
        }
    }
    // update (dt) {},
});
