/*
        添加页签 
        参数：传入内容父级div  页签栏div  iframeid属性 name  url
        和父元素等高等宽  能覆盖
*/
function addTab(contentFather, tabs, iframeid, name, url) {
    //如果url为空  则不做动作
    if (url == undefined) {
        return false;
    }
    //如果有这个页签 则把这个页签放在上层
    var tab = tabs.find('[iframeid=' + iframeid + ']');
    if (tab.length != 0) {
        showTab(contentFather, tabs, iframeid);
        return false;
    }

    //如果没有这个页签则添加页签
    var tab = $('<div class="btn btn-success btn-sm tab"  iframeid="' + iframeid + '">' + name + '&nbsp;&nbsp;<span class="glyphicon glyphicon-remove"></span></div>$nbsp;');
    tab.appendTo(tabs);
    tab.siblings().attr('class', 'btn btn-info btn-sm tab');//改变其他标签颜色
    //添加内容
    var iframeNew = $('<iframe  scrolling="auto" frameborder="0" style="position:absolute;width:100%;height:' + contentFather.height() + 'px;" iframeid="' + iframeid + '" src="' + url + '"></div>');//创建内容
    iframeNew.appendTo(contentFather);//添加
    iframeNew.siblings().css('z-index', '1');//设置其他为底层
    iframeNew.css('z-index', '9999'); //设为首层
}
/*
        点击页签
        参数：传入父级div  页签栏   iframeid属性
        把指定元素置于最上层  
*/
function showTab(contentFather, tabs, iframeid) {
    //改变当前页签颜色
    var tab = $(tabs.find('[iframeid=' + iframeid + ']')).attr('class', 'btn btn-success btn-sm tab');
    //改变其他页签颜色
    tab.siblings().attr('class', 'btn btn-info btn-sm tab');
    //把对应页置于最上层
    var iframe = $(contentFather.find('[iframeid=' + iframeid + ']'));
    iframe.siblings().css('z-index', '1');
    iframe.css('z-index', '9999');
}


/*
       关闭页面
*/
function closeTab(contentFather, tabs, iframeid) {
    //移除页面
    contentFather.find('[iframeid=' + iframeid + ']').remove();
    //移除页签
    tabs.find(('[iframeid=' + iframeid + ']')).remove();
    //获取最后一个页签 并调用点击事件(当前最前的一个关闭了 要找一个顶上)
    tabs.children("div:last-child").click();
}
/*
    *初始化控件   
    *参数 tree  放置菜单列表的div
*/
function init(contentFather, tabs, tree) {

    //注册菜单点击事件
    tree.on('click', 'li', function () {
        var url = $(this).attr('url');
        var iframeid = $(this).attr('iframeid');
        var name = $(this).text();
        addTab(contentFather, tabs, iframeid, name, url);
    })
    //注册页签点击事件
    tabs.on('click', 'div', function () {
        var iframeid = $(this).attr('iframeid');
        showTab(contentFather, tabs, iframeid);
    })
    //注册关闭按钮事件
    tabs.on('click', 'span', function () {
        var iframeid = $(this).parent().attr('iframeid');
        closeTab(contentFather, tabs, iframeid);
        return false;
    })
}
