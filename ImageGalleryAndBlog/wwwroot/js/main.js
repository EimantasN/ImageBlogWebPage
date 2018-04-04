$(document).ready(function () {
    //var obj = JSON.parse(sessionStorage.user);
    //if(obj.toString().length == 0) {
    //    var album = {'name':"city"};
    //    sessionStorage.setItem('album', JSON.stringify(user));
    //}
    //$(window).load(function () {
    //    if ($(".send-db").hasClass("active")) {
    //        db = $(".active").attr('attr').toString();

    //        send_db(db);
    //    }
    //});

    //$('.send-db').on('click', (function () {
    //    if(($(window).width() < 530)) {
    //        $(".side-grid").slideUp(500);
    //        $(".navigation ul").slideUp(500);
    //    }
    //    if(!$(this).hasClass("active")) {
    //        $(".send-db").removeClass("active");
    //        $(this).addClass('active');
    //        db = $(this).attr('attr').toString();
    //        send_db(db);
    //    }
    //}));

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

    //function send_db(db) {
    //    var i = 1;
    //    if (db.length > 0) {
    //        $.ajax({
    //            type: 'POST',
    //            url: 'Database/photo-database.php',
    //            data: {
    //                'db': db,
    //            },
    //            beforeSend:function (html) {
    //                $('#ciabus').fadeOut(100);
    //            },
    //            success:function (html) {
    //                $.when($('#ciabus').html(html)), ($('#ciabus').slideDown(1000));
    //            }
    //        });
    //    }
    //}

    $(document).on("click",
        ".img-responsive",
        function () {
            if ($(window).width() > 530) {
                id = $(this).attr("id").toString();
                //data = $(this).attr('data').toString();
                $("body").css("overflow", "hidden");
                $(".img-wiev").fadeIn(800);
                alert("turetu veikti");
                //image(id, data);
            }
        });

    //function image(id, data) {
    //    if (id.length > 0) {
    //        $.ajax({
    //            type: 'POST',
    //            url: 'Database/photo-database.php',
    //            data: {
    //                'id': id,
    //                'data': data,
    //            },
    //            beforeSend:function (html) {
    //                $('#slider').empty();
    //            },
    //            success:function (html) {
    //                $('#slider').html(html);
    //                $('.img-wiev').fadeIn(800);
    //                /*$('.outer').remove();*/
    //            }
    //        });
    //    }
    //}
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

            //console.log("gaunamas dokumento dydis"+(Height));
            //console.log("viso dokumento dydis"+($(this).height()));
            //console.log("ekrano dokumento dydis"+$(window).height());
        });

    //function eventFire(el, etype){
    //    if (el.fireEvent) {
    //        el.fireEvent('on' + etype);
    //    } else {
    //        var evObj = document.createEvent('Events');
    //        evObj.initEvent(etype, true, false);
    //        el.dispatchEvent(evObj);
    //    }
    //}

    //$(".text-album").on("click", function () {
    //    var album = {'name':document.getElementById($(this).text())};
    //    sessionStorage.setItem('album', JSON.stringify(user));
    //    setTimeout(function(){eventFire(document.getElementById($(this).text()), 'click');}, 1000);
    //    eventFire(document.getElementById($(this).text()), 'click');
    //});

    /*BLOG STYLES*/

    /*BIG ARTICLE*/
    var active_article = "";

    $(".header").on("click",
        function () {
            $("body").css("overflow", "hidden");
            $(".blog-grid").addClass("on");
            $(this).parent().parent().attr("class", "article-active");
            $(this).parent().parent().find("img")
                .attr("style", ""); //pasalina stiliu, jei kopintas is kito puslapio paveiksliukas
            $(this).parent().parent().find(".back").fadeIn();
            if (($(window).width() <= 530)) {
                $(".meniu-container").fadeOut();
            }
        });
    $(".back").on("click",
        function () {
            $(this).fadeOut();
            $("body").css("overflow", "scroll");
            $(".blog-grid").removeClass("on");
            $(".article-active").attr("class", "article");
            if (($(window).width() <= 530)) {
                $(".meniu-container").fadeIn();
            }
        });

    /*========================================*/
    /*ADMIN PANEL*/
    /*========================================*/

    $(".tablinks").on("click",
        function () {
            $("form").hide(500);
            var id = $(this).attr("link").toString();
            id = "#" + id;
            $(id).show(500);
            $(".live-url").attr("src", "");
        });

    function tabs(className) {
        $(className).removeClass("tabcontent");
    }

    $(".live-img").focusout(function () {
        patikra_fn();
    });

    function patikra_fn() {
        var img = $(".live-img").val().toString();
        $(".live-url").attr("src", img);
    }

    /*=============================================*/
    /*LIVE ARTICLE PREVIEW*/
    /*=============================================*/
});