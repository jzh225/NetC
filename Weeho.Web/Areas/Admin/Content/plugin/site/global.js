$.extend({
    popupIframe: function (title, url, callback, size) {
        if (size === undefined || size == null)
            size = ["60%", "80%"];
        layer.closeAll();
        layer.open({
            //btn: ['取消'],
            //btnAlign: 'r',
            type: 2,
            title: title,
            shadeClose: true,
            shade: 0.8,
            area: size,
            content: url, //iframe的url
            end: function () {
                if (callback)
                    callback();
                //else
                //    window.location = window.location;
            },
            //btn1:function (index) {
            //    layer.close(index);
            //}
        });
    },
    loading: function () {
        layer.load(2);
    },
    alert: function (msg, callback) {
        layer.closeAll();

        layer.alert(msg, {
            end: function () {
                if (callback)
                    callback();
                //else
                //    window.location = window.location;
            }
        });
    },
    msg: function (msg) {
        layer.msg(msg);
    },
    open: function (obj) {
        layer.open({
            type: 1,
            shade: false,
            title: false, //不显示标题
            content: obj, //捕获的元素
            cancel: function () {

            }
        });
    },
    popup: function (obj, callback, title) {
        layer.closeAll();
        layer.open({
            type: 1,
            title: title ? title : false,
            closeBtn: 0,
            shadeClose: true,
            skin: 'yourclass',
            content: (obj instanceof jQuery ? obj.html() : obj),
            end: function () {
                if (callback)
                    callback();
            },
            success: function () {

            }
        });

    },
    popupFull: function (obj) {
        layer.closeAll();
        layer.open({
            type: 1,
            title: false,
            closeBtn: 1,
            shadeClose: true,
            skin: 'text-center',
            area: ["90%", "90%"],
            content: (obj instanceof jQuery ? obj.html() : obj)

        });
    },
    confirm: function (msg, caller) {
        layer.msg(msg, {
            time: 0,
            btn: ['确定', '取消'],
            yes: function (index) {
                layer.close(index);
                if (caller) {
                    caller();
                }
            }
        });
    },

    setQueryString: function (uri, key, value) {
        var re = new RegExp("([?&])" + key + "=.*?(&|$)", "i");
        var separator = uri.indexOf('?') !== -1 ? "&" : "?";
        if (uri.match(re)) {
            return uri.replace(re, '$1' + key + "=" + value + '$2');
        }
        else {
            return uri + separator + key + "=" + value;
        }
        return uri;
    }
});
$(function () {
    $(document).on("click", ".btn-post", function () {
        var control = $(this), confirm = true;
        if (control.attr("confirm") == undefined) {
            confirm = false;
        }
        function action() {
            $.loading();

            $.post(control.data("url"), eval("(" + control.data("params") + ")"), function (response) {
                if (response.success) {
                    $.alert("操作成功", function () {
                        var returnUrl = control.data("return");
                        if (returnUrl)
                            window.location = returnUrl;
                        else
                            window.location = window.location;
                    });
                } else {
                    $.alert(response.msg);
                }
            });
        }
        if (confirm) {
            $.confirm("确定进行此操作吗？", function () {
                action();
            });
        } else {
            action();
        }
    });
    $(document).on("click", ".btn-post-check", function () {
        var control = $(this), confirm = true;
        if (control.attr("confirm") == undefined) {
            confirm = false;
        }
        function action() {
            var values = getCheckBoxVal(getElementsByName("input", "check-item"));
            if (values.length == 0) {
                $.alert("请选择项");
                return;
            }
            $.loading();
            var params = merge_options({ ids: values }, eval("(" + control.data("params") + ")"));
            $.post(control.data("url"), params, function (response) {
                if (response.success) {
                    $.alert("操作成功", function () {
                        window.location = window.location;
                    });
                } else {
                    $.alert(response.msg);
                }
            });
        }
        if (confirm) {
            $.confirm("确定进行此操作吗？", function () {
                action();
            });
        } else {
            action();
        }
    });
    $(document).on("click", ".btn-post-check2", function () {
        var control = $(this), confirm = true;
        if (control.attr("confirm") == undefined) {
            confirm = false;
        }
        function action() {
            $.loading();
            var params = {}
            params.userids = Values1.join(",");
            params.couponids = Values2.join(",");
            $.post(control.data("url"), params, function (response) {
                if (response.success) {
                    $.alert("优惠券发放成功", function () {
                        window.location = window.location;
                    });
                } else {
                    $.alert(response.msg);
                }
            });
        }
        if (confirm) {
            var Values1 = getCheckBoxVal(getElementsByName("input", "check-item"));
            if (Values1.length == 0) {
                $.alert("请选择发放用户");
                return;
            }
            var Values2 = getCheckBoxVal(getElementsByName("input", "check-item2"));
            if (Values2.length == 0) {
                $.alert("请选择发放的优惠券");
                return;
            }
            $.confirm("确定进行此操作吗？", function () {
                action();
            });
        } else {
            action();
        }
    });
    $(".btn-frame-Jump").click(function () {
        var obj = $(this);
        var values = getCheckBoxVal(getElementsByName("input", "check-item"));
        if (values.length == 0) {
            $.alert("请选择项");
            return;
        }
        //var params = merge_options({ ids: values }, eval("(" + control.data("params") + ")"));
        $.popupIframe(obj.data("title"), obj.data("href") + "?" + values.join("-"), null, eval("(" + obj.data("size") + ")"));
        return false;
    });
    $(".main-right").css("width", $(window).width() - 260);
    $(".checkAll").click(function () {
        var toggle = $(this);
        var elem = getElementsByName("input", "check-item");
        for (i = 0, iarr = 0; i < elem.length; i++) {
            elem[i].checked = toggle[0].checked;
        }

    });
    $(".checkAll2").click(function () {
        var toggle = $(this);
        var elem = getElementsByName("input", "check-item2");
        for (i = 0, iarr = 0; i < elem.length; i++) {
            elem[i].checked = toggle[0].checked;
        }

    });
    $(".btn-frame").click(function () {
        var obj = $(this);
        $.popupIframe(obj.attr("title"), obj.attr("href"), null, eval("(" + obj.attr("size") + ")"));
        return false;
    });
    $(".btn-return").click(function () {
        if (inIframe()) {
            window.parent.location = window.parent.location;
        } else {
            window.location = window.location;
        }
    });
    $("table td>img").click(function (event) {
        event.stopPropagation();
        var that = $(this);
        $.popupFull("<img src='" + that.attr("src") + "' />");
    });
    $(".sidebar-toggle").click(function () {
        var that = $(this), nav = that.next(".nav"), li = that.parent();
        if (nav.hasClass("hide")) {
            li.removeClass("plus").addClass("minus");
            nav.removeClass("hide");
        } else {
            li.addClass("plus").removeClass("minus");
            nav.addClass("hide");
        }
    });
    $(window).scroll(function () {
        var obj = $(".btn-group");
        var a = obj.offset().top;
        var scrolls = $(window).scrollTop();
        if (a >= scrolls && a < (scrolls + $(window).height())) {
            if (!obj.hasClass("static"))
                obj.css({
                    position: "static",
                });
        } else {
            if (!obj.hasClass("fixed"))
                obj.css({
                    position: "fixed",
                    bottom: 0,
                    "z-index": 1000
                });
        }
    });
});
function merge_options(obj1, obj2) {
    var obj3 = {};
    for (var attrname in obj1) { obj3[attrname] = obj1[attrname]; }
    for (var attrname in obj2) { obj3[attrname] = obj2[attrname]; }
    return obj3;
}
function getElementsByName(tag, name) {
    var elem = document.getElementsByTagName(tag);
    var arr = new Array();
    for (i = 0, iarr = 0; i < elem.length; i++) {
        att = elem[i].getAttribute("name");

        if (att == name) {
            arr[iarr] = elem[i];
            iarr++;
        }
    }
    return arr;
}
function getCheckBoxVal(elements) {
    var value = [], j = 0;
    for (i = 0; i < elements.length; i++) {
        if (elements[i].checked) {
            value[j] = elements[i].value;
            j++;
        }
    }
    //return value.join(",");
    return value;
}
function autoResizeFrame() {
    var newheight = document.getElementById("iframe").contentWindow.document.body.scrollHeight;
    if (newheight > $(window).height() - 100) {
        newheight = $(window).height() - 100;
    }
    var width = 600;
    if ($('.table,.col-md-6', $("#iframe").contents()).length > 0) {
        $("#iframe").css({ height: $(window).height() - 110, width: "100%" });
        $(".frame").css({ "height": $(window).height() - 50, "width": "90%", "left": "5%", "top": 20 });
    } else {
        $("#iframe").css({ height: newheight, width: width });
        $(".frame").css({ "height": newheight + 52, "width": width, "left": ($(window).width() - width) / 2, "top": ($(window).height() - newheight - 52) / 2 });
    }

}
function inIframe() {
    try {
        return window.self !== window.top;
    } catch (e) {
        return true;
    }
}