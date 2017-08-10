$(function(){
    var form = $("form");
    form.validate();
    form.unbind("submit");
    form.bind("submit", function () {
        if (form.isValid()) {
            $.loading();
            $.post(form.attr("action"), form.serialize(), function (response) {
                if (response.status == "1") {
                    $.alert("操作成功", function () {
                        window.location = $.setQueryString(window.location.href, "id", response.result);
                    });
                } else {
                    $.alert(response.result);
                }
            });
            return false;
        }
        return false;
    });
})
