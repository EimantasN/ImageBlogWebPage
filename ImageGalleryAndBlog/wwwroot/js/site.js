$(document).ready(function () {
    //Nustato ar rodyti mini-meniu
    if ($(window).width() > 530) {
        $(".meniu-container").hide();
    } else {
        $(".meniu-container").show();
    }
    //neberodyti navigation kai width maziau uz 530
    $(window).resize(function () {
        if ($(window).width() > 530 && !$("div").hasClass("article-active")) {
            $(".navigation ul").slideDown();
            $(".meniu-container").hide();
        }
        if (($(window).width() < 530) && !$("div").hasClass("article-active")) {
            //css navigation display none in responsive
            $(".meniu-container").show();
        }
        if ($(window).width() < $(window).height()) {
            $(".img-wiev").fadeOut(500);
        }
    });

    $(".meniu-container").on("click",
        function () {
            $(".navigation ul").slideToggle(300);
            $(".dropdown-content").slideUp();
            $(this).find(".down-caret").removeClass("open-caret");
        });

    $(".dropdown").on("click",
        function () {
            $(this).find(".dropdown-content").slideToggle(300);
            $(this).find(".down-caret").toggleClass("open-caret");
        });

    $(document).on("click",
        ".img-responsive",
        function () {
            if ($(window).width() > 530) {
                Id = $(this).attr("id").toString();
                setChecked(Id);
                //data = $(this).attr('data').toString();
                $("body").css("overflow", "hidden");
                $(".img-wiev").fadeIn(800);
            }
        });

    function setChecked(Id) {
        for (var i = 0; i < parseInt(Id); i++) {
            imgId = "img-" + i;
            $("#" + imgId).replaceWith("<input type='radio' name='radio-btn' id='img-" + i + "'>");
        }
        imgId = "img-" + Id;
        $("#" + imgId).replaceWith("<input type='radio' name='radio-btn' checked='checked' id='" + imgId + "'>");
    };

    $(".close-classic").on("click",
        function () {
            $(".img-wiev").fadeOut(500);
            $("body").css("overflow", "scroll");
        });

    // Reikia pakeisti kad reguotu i pixelius
    // Pirmiausia sumazina "IEVA PHOTOS", o kai pasieke 150px tada isnyskta ir navbar
    $(".img-grid").on("scroll",
        function () {
            $("body").css("overflow", "hidden");

            //Nusistatome kiek px pajudeta ir palei sitai nustatau kiek logo rodyti ir kada ji uzdengti
            var Height = 150 - $(this).scrollTop();
            $(".logo").css("height", Height + "px");

            var Height2 = $(this).height() + Height;
            //Jei LOGO nesimato, isjungiame
            if (Height < 0) {
                $(".logo").hide();
            }
            if (Height >= 0) {
                $(this).css("height", $(window).height() - 45 + "px");
            } else {
                $(".logo").show();
            }
        });

    /*========================================*/
    /*PAGNATION BLOG*/
    /*========================================*/

    var articlesPerPage = 5;
    var arcCount = $(".blog-grid article").length;

    if (articlesPerPage <= arcCount) {
        var pslCount;
        if ((arcCount % articlesPerPage) === 0) {
            pslCount = arcCount / articlesPerPage;
        } else if ((arcCount % articlesPerPage) <= 0.5) {
            pslCount = Math.round(arcCount / articlesPerPage);
        } else {
            pslCount = Math.round((arcCount / articlesPerPage));
            pslCount++;
        }
        $(".pag").append("<li class='pag-item pag-active' id=" + 1 + "><a class='pag-link'>" + 1 + "</a></li>");
        for (var i = 2; i <= pslCount; i++) {
            $(".pag").append("<li class='pag-item' id=" + i + "><a class='pag-link'>" + i + "</a></li>");
        }

        var page = 1;
        pagination(page);
        articleShow(page);
        $(".pag-item").on("click",
            function() {
                page = $(this).attr("id").toString();
                $(".pag li").removeClass("pag-active");
                $(this).addClass("pag-active");
                pagination(parseInt(page));
                articleShow(page);
                $("html, body").animate({ scrollTop: 0 }, "slow");
            });

        function pagination(current) {
            var i;
            if (pslCount >= 3) {
                var first;
                var last;
                if ((current + 2) < pslCount) {
                    first = current - 1;
                    last = current + 2;
                } else {
                    first = pslCount - 3;
                    last = pslCount;
                }
                $(".pagination li").hide();
                for (i = first; i <= last; i++) {
                    $(".pag li:eq(" + (i - 1) + ")").show();
                }
            } else {
                $(".pag li").hide();
                for (i = 1; i <= pslCount; i++) {
                    $(".pag li:eq(" + (i - 1) + ")").show();
                }
            }
        }
    } else {
        articleShow(1);
    }

    function articleShow(page) {
        // if(page !== 1) {
        var pradzia = articlesPerPage * (page - 1);
        var pabaiga = pradzia + articlesPerPage;

        $("article").hide();

        for (var i = pradzia; (i < pabaiga) && (i <= arcCount); i++) {
            $("article:eq(" + i + ")").show();
        }
    }

    /*=============================================*/
    /*CUSTOM NICE SCROOL*/
    /*===========================================*/

    $("html").niceScroll({
        styler: "fb",
        cursorcolor: "#5e6d6d",
        cursorwidth: "5",
        cursorborderradius: "5px",
        background: "#5e6d6d",
        spacebarenabled: false,
        cursorborder: "0",
        zindex: "1000"
    });

    $(".img-grid").niceScroll({
        styler: "fb",
        cursorcolor: "#5e6d6d",
        cursorwidth: "3",
        cursorborderradius: "5",
        background: "#5e6d6d",
        spacebarenabled: false,
        cursorborder: "0"
    });
});