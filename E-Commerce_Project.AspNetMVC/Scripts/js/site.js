$(document).ready(function () {
    $(".citem").mouseover(function () {
        $(this).find(".cartproductadditem").css("visibility", "visible")
    });

    $(".citem").mouseleave(function () {
        $(this).find(".cartproductadditem").css("visibility", "hidden")
    });

    $(".productimg").mouseover(function () {

        $("#productimgshow").attr("src", $(this).attr("src"));
    });

    $("#yorumlar").click(function () {
        $("#nav-yorumlar-tab").trigger("click");
        $('html, body').animate({
            scrollTop: $("#nav-tab").offset().top
        });
    });

    $("#star1").mouseover(function () {
        $("#star1").removeClass("far").addClass("fas");
        $("#star2").removeClass("fas").addClass("far");
        $("#star3").removeClass("fas").addClass("far");
        $("#star4").removeClass("fas").addClass("far");
        $("#star5").removeClass("fas").addClass("far");
    });

    $("#star1").mouseleave(function () {
        $(this).removeClass("fas").addClass("far");
        starleave();
    });

    $("#star2").mouseover(function () {
        $("#star1").removeClass("far").addClass("fas");
        $("#star2").removeClass("far").addClass("fas");
        $("#star3").removeClass("fas").addClass("far");
        $("#star4").removeClass("fas").addClass("far");
        $("#star5").removeClass("fas").addClass("far");
    });

    $("#star2").mouseleave(function () {
        $("#star1").removeClass("fas").addClass("far");
        $(this).removeClass("fas").addClass("far");
        starleave();
    });

    $("#star3").mouseover(function () {
        $("#star1").removeClass("far").addClass("fas");
        $("#star2").removeClass("far").addClass("fas");
        $("#star3").removeClass("far").addClass("fas");
        $("#star4").removeClass("fas").addClass("far");
        $("#star5").removeClass("fas").addClass("far");
    });

    $("#star3").mouseleave(function () {
        $("#star1").removeClass("fas").addClass("far");
        $("#star2").removeClass("fas").addClass("far");
        $(this).removeClass("fas").addClass("far");
        starleave();
    });

    $("#star4").mouseover(function () {
        $("#star1").removeClass("far").addClass("fas");
        $("#star2").removeClass("far").addClass("fas");
        $("#star3").removeClass("far").addClass("fas");
        $("#star4").removeClass("far").addClass("fas");
        $("#star5").removeClass("fas").addClass("far");
    });

    $("#star4").mouseleave(function () {
        $("#star1").removeClass("fas").addClass("far");
        $("#star2").removeClass("fas").addClass("far");
        $("#star3").removeClass("fas").addClass("far");
        $(this).removeClass("fas").addClass("far");
        starleave();
    });


    $("#star5").mouseover(function () {
        $("#star1").removeClass("far").addClass("fas");
        $("#star2").removeClass("far").addClass("fas");
        $("#star3").removeClass("far").addClass("fas");
        $("#star4").removeClass("far").addClass("fas");
        $(this).removeClass("far").addClass("fas");
    });

    $("#star5").mouseleave(function () {
        $("#star1").removeClass("fas").addClass("far");
        $("#star2").removeClass("fas").addClass("far");
        $("#star3").removeClass("fas").addClass("far");
        $("#star4").removeClass("fas").addClass("far");
        $(this).removeClass("fas").addClass("far");
        starleave();
    });

    function starleave() {
        if (click1) {
            $("#star1").removeClass("far").addClass("fas");
        }
        else if (click2) {
            $("#star1").removeClass("far").addClass("fas");
            $("#star2").removeClass("far").addClass("fas");
        }
        else if (click3) {
            $("#star1").removeClass("far").addClass("fas");
            $("#star2").removeClass("far").addClass("fas");
            $("#star3").removeClass("far").addClass("fas");
        }
        else if (click4) {
            $("#star1").removeClass("far").addClass("fas");
            $("#star2").removeClass("far").addClass("fas");
            $("#star3").removeClass("far").addClass("fas");
            $("#star4").removeClass("far").addClass("fas");
        }
        else if (click5) {
            $("#star1").removeClass("far").addClass("fas");
            $("#star2").removeClass("far").addClass("fas");
            $("#star3").removeClass("far").addClass("fas");
            $("#star4").removeClass("far").addClass("fas");
            $("#star5").removeClass("far").addClass("fas");
        }
    }

    var click1 = false;
    var click2 = false;
    var click3 = false;
    var click4 = false;
    var click5 = false;

    $("#star1").click(function () {
        $("#point").val("1");
        $("#btn-comment").removeAttr("Disabled");
        click1 = true;
        click2 = false;
        click3 = false;
        click4 = false;
        click5 = false;
    });

    $("#star2").click(function () {
        $("#point").val("2");
        $("#btn-comment").removeAttr("Disabled");
        click1 = false;
        click2 = true;
        click3 = false;
        click4 = false;
        click5 = false;
    });

    $("#star3").click(function () {
        $("#point").val("3");
        $("#btn-comment").removeAttr("Disabled");
        click1 = false;
        click2 = false;
        click3 = true;
        click4 = false;
        click5 = false;
    });

    $("#star4").click(function () {
        $("#point").val("4");
        $("#btn-comment").removeAttr("Disabled");
        click1 = false;
        click2 = false;
        click3 = false;
        click4 = true;
        click5 = false;
    });

    $("#star5").click(function () {
        $("#point").val("5");
        $("#btn-comment").removeAttr("Disabled");
        click1 = false;
        click2 = false;
        click3 = false;
        click4 = false;
        click5 = true;
    });

    var dt = (new Date);
    var day = dt.getDate();
    var month = dt.getMonth() + 1;
    var year = dt.getFullYear();
    var date = (day < 10 ? "0" : "") + day + "." + (month < 10 ? "0" : "") + month + "." + year;
    $("#commentdate").text(date);

    var itemsMainDiv = ('.MultiCarousel');
    var itemsDiv = ('.MultiCarousel-inner');
    var itemWidth = "";

    $('.leftLst, .rightLst').click(function () {
        var condition = $(this).hasClass("leftLst");
        if (condition)
            click(0, this);
        else
            click(1, this)
    });

    ResCarouselSize();

    $(window).resize(function () {
        ResCarouselSize();
    });

    function ResCarouselSize() {
        var incno = 0;
        var dataItems = ("data-items");
        var itemClass = ('.item');
        var id = 0;
        var btnParentSb = '';
        var itemsSplit = '';
        var sampwidth = $(itemsMainDiv).width();
        var bodyWidth = $('body').width();
        $(itemsDiv).each(function () {
            id = id + 1;
            var itemNumbers = $(this).find(itemClass).length;
            btnParentSb = $(this).parent().attr(dataItems);
            itemsSplit = btnParentSb.split(',');
            $(this).parent().attr("id", "MultiCarousel" + id);


            if (bodyWidth >= 1200) {
                incno = itemsSplit[3];
                itemWidth = sampwidth / incno;
            }
            else if (bodyWidth >= 992) {
                incno = itemsSplit[2];
                itemWidth = sampwidth / incno;
            }
            else if (bodyWidth >= 768) {
                incno = itemsSplit[1];
                itemWidth = sampwidth / incno;
            }
            else {
                incno = itemsSplit[0];
                itemWidth = sampwidth / incno;
            }
            $(this).css({ 'transform': 'translateX(0px)', 'width': itemWidth * itemNumbers });
            $(this).find(itemClass).each(function () {
                $(this).outerWidth(itemWidth);
            });

            $(".leftLst").addClass("over");
            $(".rightLst").removeClass("over");

        });
    }

    function ResCarousel(e, el, s) {
        var leftBtn = ('.leftLst');
        var rightBtn = ('.rightLst');
        var translateXval = '';
        var divStyle = $(el + ' ' + itemsDiv).css('transform');
        var values = divStyle.match(/-?[\d\.]+/g);
        var xds = Math.abs(values[4]);
        if (e == 0) {
            translateXval = parseInt(xds) - parseInt(itemWidth * s);
            $(el + ' ' + rightBtn).removeClass("over");

            if (translateXval <= itemWidth / 2) {
                translateXval = 0;
                $(el + ' ' + leftBtn).addClass("over");
            }
        }
        else if (e == 1) {
            var itemsCondition = $(el).find(itemsDiv).width() - $(el).width();
            translateXval = parseInt(xds) + parseInt(itemWidth * s);
            $(el + ' ' + leftBtn).removeClass("over");

            if (translateXval >= itemsCondition - itemWidth / 2) {
                translateXval = itemsCondition;
                $(el + ' ' + rightBtn).addClass("over");
            }
        }
        $(el + ' ' + itemsDiv).css('transform', 'translateX(' + -translateXval + 'px)');
    }

    function click(ell, ee) {
        var Parent = "#" + $(ee).parent().attr("id");
        var slide = $(Parent).attr("data-slide");
        ResCarousel(ell, Parent, slide);
    }

    $("#customCollapseExample").css("display", "none")

    $("#customRadio3").click(function () {
        $("#customCollapseExample").css("display", "block")
    });

    if ($("#commentTab").val() == "True") {
        $("#nav-yorumlar-tab").trigger("click");
        $(window).scrollTop($("#nav-tab").offset().top);
    }
});

$(window).resize(function () {
    var size = $(window).width();
    if (size < 751) {
        $("#allcategories").html("Tüm Kategoriler  <i class='fas fa-sort-down'></i>")
    }
    else {
        $("#allcategories").html("Diğer Kategoriler  <i class='fas fa-sort-down'></i>")
    }
});