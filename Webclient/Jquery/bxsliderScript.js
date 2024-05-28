//home page main banner
$(document).ready(function ($) {

    var bxSlider = document.getElementById("bxSlider");
    if (bxSlider) {
        var homepageSliderConfig = {
            responsive: true,
            auto: false,
            controls: true,
            hideControlOnEnd: true,
            speed: 2000,
            startSlide: 0,
            autoControls: true,
            pager: true,
            infiniteLoop: true,
            touchEnabled: false,
            adaptiveHeight: true,
            touchEnabled: false
        };
        var indexBannerCollection = new Array();
        $(document).on("langSelectorEvent", {
            page: 'index-banner'
        }, function (event, lang) {
            return pageContentSwitcher(event, lang).done(function (d) {
                // Purge last sliders properly
                $.each(indexBannerCollection, function (i, slider) {
                    if (slider != undefined && slider.destroySlider != undefined) {
                        slider.destroySlider();
                    }
                });
                indexBannerCollection = new Array();
                // Create new DOM slider
                var id = 'slider' + UniqueId();
                document.getElementById('bxSlider').innerHTML = "";
                document.getElementById('bxSlider').append(createSliderDOM(id, d));
                var $homeSlider = $('#' + id).bxSlider(homepageSliderConfig);
                // Add banner to collection
                indexBannerCollection.push($homeSlider);
            });
        });
    }

    //
    var homepageScroller = {
        pager: false,
        controls: true,
        auto: false,
        slideWidth: 5000,
        startSlide: 0,
        nextText: ' ',
        prevText: ' ',
        infiniteLoop: false,
        adaptiveHeight: true,
        hideControlOnEnd: true,
        moveSlides: 1,
        slideMargin: 20,
        touchEnabled: false
    };
    //
    var $bxSliderRO = $('.bxSliderRO').bxSlider(buildSliderConfiguration(homepageScroller));
    var bxSliderOP = $('.bxSliderOP').bxSlider(buildSliderConfiguration(homepageScroller));
    //
    window.addEventListener("orientationchange", function () {
        reloadSlider($bxSliderRO, buildSliderConfiguration(homepageScroller));
        reloadSlider(bxSliderOP, buildSliderConfiguration(homepageScroller));
    }, false);
    window.addEventListener("resize", function () {
        reloadSlider($bxSliderRO, buildSliderConfiguration(homepageScroller));
        reloadSlider(bxSliderOP, buildSliderConfiguration(homepageScroller));
    }, false);
});

var UniqueId = function () {
    return '_' + Math.random().toString(36).substr(2, 9);
};

function createSliderDOM(id, content) {
    var ul = document.createElement("ul");
    ul.setAttribute('id', id);
    ul.innerHTML = content;
    return ul;
}

function buildSliderConfiguration(baseConfig) {
    var windowWidth = $(window).width();
    var numberOfVisibleSlides;
    if (windowWidth < 420) {
        numberOfVisibleSlides = 1;
    } else if (windowWidth < 768) {
        numberOfVisibleSlides = 2;
    } else if (windowWidth < 1200) {
        numberOfVisibleSlides = 3;
    } else {
        numberOfVisibleSlides = 4;
    }
    //
    var target = {};
    $.extend(target, {
        minSlides: numberOfVisibleSlides,
        maxSlides: numberOfVisibleSlides
    }, baseConfig);
    return target;
}

function reloadSlider($slider, config) {
    if ($slider != undefined && $slider.reloadSlider !== undefined) {
        $slider.reloadSlider(config);
        sliderNavigation($slider);
    }
}

function sliderNavigation($BXSliderInit) {
    $('.slider-prev').click(function () {
        var current = $BXSliderInit.getCurrentSlide();
        $BXSliderInit.goToPrevSlide(current) - 1;
    });
    //
    $('.slider-next').click(function () {
        var current = $BXSliderInit.getCurrentSlide();
        $BXSliderInit.goToNextSlide(current) + 1;
    });
}
