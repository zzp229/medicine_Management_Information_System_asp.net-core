﻿function exeData(num, type) {
    loadData(num);
}
function loadpage() {
    var myPageCount = parseInt($("#PageCount").val());
    var myPageSize = parseInt($("#PageSize").val());
    var countindex = myPageCount % myPageSize > 0 ? (myPageCount / myPageSize) + 1 : (myPageCount / myPageSize);
    $("#countindex").val(countindex == 0 ? 1 : countindex);

    $.jqPaginator('#pagination', {
        totalPages: parseInt($("#countindex").val()),
        visiblePages: parseInt($("#visiblePages").val()),
        currentPage: 1,
        first: '<li class="first page-item"><a class="page-link" href="javascript:;">首页</a></li>',
        prev: '<li class="prev page-item"><a class="page-link" href="javascript:;"><i class="arrow arrow2"></i>上一页</a></li>',
        next: '<li class="next page-item"><a class="page-link" href="javascript:;">下一页<i class="arrow arrow3"></i></a></li>',
        last: '<li class="last page-item"><a class="page-link" href="javascript:;">末页</a></li>',
        page: '<li class="page page-item"><a class="page-link" href="javascript:;">{{page}}</a></li>',
        onPageChange: function (num, type) {
            console.log(num)
            console.log(type)
            if (type == "change") {
                exeData(num, type);
            }
        }
    });
} 
