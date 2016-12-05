$(function () {
    if (window != top) {
        top.location.href = location.href;
    }
    var appName = navigator.appName.toLowerCase();
    $("#login").addClass("current");
    $('#user').focus();
    /**
     * input 按键事件keyup
     */
    $("#user").keyup(function (e) {
        if (e.keyCode == 13) {
            $('#pwd').focus();
        }
        if ($.trim($(this).val()) != "") {
            //灵活的提示框，打字就隐藏
            $('#showMsg').css('display','none');
        }
    });
    $("#pwd").keyup(function (e) {
        if (e.keyCode == 13) {
            $("#checkcode").focus();
        }
        if ($.trim($(this).val()) != "") {
            $('#showMsg').css('display', 'none');
        }
    });
    $("#checkcode").keyup(function (e) {
        if (e.keyCode == 13) {
            Loging();
        }
        if ($.trim($(this).val()) != "") {
            $('#showMsg').css('display', 'none');
        }
    });
    /**
     * 登录按钮
     */
    $("#btnLogin").click(Loging);
});

function Loging() {
    var username = $("#user").val();
    if (username == "") {
        $('#showMsg').text('账号不能为空');
        $('#showMsg').css('display', 'block');
        $("#user").focus();
        return false;
    }
    var password = $("#pwd").val();
    if (password == "") {
        $('#showMsg').text('密码不能为空');
        $('#showMsg').css('display', 'block');
        $("#pwd").focus();
        return false;
    }
    var checkcode = $("#checkcode").val();
    if (checkcode == "") {
        $('#showMsg').text('验证码不能为空');
        $('#showMsg').css('display', 'block');
        $("#checkcode").focus();
        return false;
    }
    $('#btnLogin').html("正在登陆...");
    $('#btnLogin').disabled = true;
    $.ajax({
        url: '/Login/Login/',
        async: true,
        type: 'POST',
        dataType: 'text',
        data: {
            //uid: encodeURIComponent(username),
            //pwd: encodeURIComponent(password),
            uid: username,
            pwd: password,
            checkcode:checkcode
        },
        success: function (state) {
            $('#btnLogin').disabled = false;
            if (state == '0') {
                $('#btnLogin').html("登录成功");
                top.location = '/Home/Index';
                $('#btnLogin').html("正在跳转");
            }
            else if (state == '1') {
                $('#btnLogin').html("登录");
                $('#showMsg').text('用户名不存在');
                $('#showMsg').css('display', 'block');
                $("#user").focus();
            }
            else if (state == '3') {
                $('#btnLogin').html("登录");
                $('#showMsg').text('账户已锁');
                $('#showMsg').css('display', 'block');
                $("#user").focus();
            }
            else if (state == '2') {
                $('#btnLogin').html("登录");
                $('#showMsg').text('密码错误');
                $('#showMsg').css('display', 'block');
                $("#pwd").focus();
            }
            else if (state == '4') {
                $('#btnLogin').html("登录");
                $('#showMsg').text('验证码错误');
                $('#showMsg').css('display', 'block');
                $("#checkcode").focus();
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $('#btnLogin').html("登录");
            $('#showMsg').text('登录失败请稍后再试');
            $('#showMsg').css('display', 'block');
        }
    });
}
function getNewCheckCode() {
    $('#lyVcodeImg').attr('src', '/Login/GetVcode?d=' + new Date().getTime());
    $("#checkcode").val("");
}

