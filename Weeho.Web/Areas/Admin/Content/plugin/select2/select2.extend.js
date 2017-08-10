$(function () {
    $(".form-select").each(function () {
        var select = $(this);
        if (select.data("url")) {
            if (select.attr("search") === undefined) {
                $.fn.select2.defaults.set("minimumResultsForSearch", "-1");
            }
            select.select2({
                allowClear: select.attr("clear") != undefined,
                language: "zh-CN",
                placeholder: select.attr("placeholder"),
                ajax: {
                    url: select.data("url"),
                    type: "POST",
                    dataType: 'json',
                    delay: 250,
                    data: function (params) {
                        var queryParams = {
                            keyword: params.term,
                            page: params.page,
                            page_limit: 15
                        };
                        if (select.data("params")) {
                            eval("(queryParams." + select.data("params") + ")");
                        }
                        return queryParams;
                    },
                    processResults: function (data, params) {
                        params.page = params.page || 1;
                        return {
                            results: data,
                            pagination: {
                                //more: (params.page * 15) < data.total_count
                                more: true
                            }
                        };
                    }
                },
                cache: true
            })
        } else {
            select.select2({
                allowClear: select.attr("clear") != undefined,
                language: "zh-CN",
                placeholder: select.attr("placeholder")
            })
        }
        if (select.data("relate")) {
            select.on("select2:unselecting", function (e) {
                $(select.data("relate")).select2('val', '');
            });
            select.on("select2:select", function (e) {
                $(select.data("relate")).select2('val', '');
            });
        }
    });
})