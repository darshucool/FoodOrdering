$(document).ready(function () {

    
    // ---- PreFilled   ----------------------------------------------------------------------------------------------------------
    $.fn.preFilled = function () {
        $(this).focus(function () {
            if (this.value == this.defaultValue) {
                this.value = "";
            }
            $(this).addClass('focus');
        }).blur(function () {
            if (!this.value.length) {
                this.value = this.defaultValue;
                $(this).removeClass('focus');
            }

        });
    };

    $(".form-name,.form-email,.form-phone,.form-comments").preFilled();
});


$(function () {
    initBrowserDetect();
    VSA_initScrollbars();
    initPage();
    //Check to see if we're in CMSDesk. If so, don't run this script.
    if ($('.ImageSelectorHeader').length) {

    }
    else {
        desaturateImages();
    }
    //desaturateProjectItemImages();
    initProductsGallery();
    initBrochuresGallery();
    initProjectGallery();
    initAccordion();
})

// accordion
function initAccordion() {
    $('.accordion').each(function () {
        var _holder = $(this);
        var _listEls = _holder.find('>li');
        var _animSpeed = 250;
		

        _listEls.each(function (ind) {
            var _link = $(this),
				_div = _link.find('.holder'),
				_el = _link.find('a'),
				_span = _link.find('span').css({ 'z-index': 10 }),
				_img = _el.find('img'),
				_t, _fader;

            var _minW = _link.width() - 1;
            var _maxW = _img.width();
            var _diff = (_maxW - _minW) / 2;
            var _kf = 1;
            var _clone = _img.clone().insertAfter(_img);
            if (_img.get(0).complete) {
                _clone.grayscale();
                _fader = $('<div>').insertAfter(_img).css({
                    'position': 'absolute',
                    'top': 0,
                    'left': 0,
                    'width': _img.width(),
                    'height': _link.height(),
                    'background': '#ff0000',
                    'opacity': 0.5,
                    'z-index': 10
                });
            }
            else _img.get(0).onload = function () {
                _clone.grayscale();
                _fader = $('<div>').insertAfter(_img).css({
                    'position': 'absolute',
                    'top': 0,
                    'left': 0,
                    'width': _img.width(),
                    'height': _link.height(),
                    'background': '#ff0000',
                    'opacity': 0.5,
                    'z-index': 10
                });
            }
            _img.css({
                'position': 'absolute',
                'top': 0,
                'left': 0,
                'opacity': 0,
                'z-index': 5
            });

            if (ind == 0) _kf = 0;
            if (ind == _listEls.length - 1) _kf = 0;
            _link.css({
                'width': _link.width() - 1,
                'height': _link.height(),
                'z-index': 50
            })
            _el.css({
                'margin-left': -_diff * _kf,
                'margin-right': 0
            })
            _div.css({
                'position': 'absolute',
                'top': 0,
                'left': 0,
                'width': _link.width(),
                'overflow': 'hidden'
            })
            _link.mouseenter(function () {
                if (_t) clearTimeout(_t);
                _t = setTimeout(function () {
                    _link.css('z-index', 100);
                    _span.fadeIn(_animSpeed);
                    _div.css({
                        'border-left': '2px solid #fff',
                        'border-right': '2px solid #fff',
                        'left': 0
                    })
                    _div.stop().animate({
                        'left': 0,
                        'width': _maxW
                    }, _animSpeed)
                    _el.stop().animate({
                        'margin-left': 0
                    }, _animSpeed)
                    _img.stop().animate({
                        'opacity': 1
                    }, _animSpeed);
                    _fader.stop().animate({
                        'opacity': 0
                    }, _animSpeed);
                }, 100)
            }).mouseleave(function () {
                if (_t) clearTimeout(_t);
                _t = setTimeout(function () {
                    _link.css('z-index', 99);
                    _span.fadeOut(_animSpeed);
                    _div.stop().animate({
                        'left': 0,
                        'width': _minW
                    }, _animSpeed, function () {
                        _link.css('z-index', 50);
                        _div.css({
                            'border': 'none',
                            'left': 0
                        });
                    })
                    _el.stop().animate({
                        'margin-left': -_diff * _kf
                    }, _animSpeed)
                    _img.stop().animate({
                        'opacity': 0
                    }, _animSpeed);
                    _fader.stop().animate({
                        'opacity': 0.5
                    }, _animSpeed);
                }, 100)
            })
        })
    })
}



function initProjectGallery() {
	//Check to see if in CMSDesk
	if ($('.PagePlaceholderHeader').length) {
		//Do nothing - or run alternate script
	}
	else {
		//Run original script
		$('.ad-gallery').adGallery();
	}	
}



function initProductsGallery() {
    $('.products').each(function () {
        var _holder = $(this);
        var _gallery = $('.carousel');

        $(_gallery).each(function () {
            if ($(this).find('li').length > 7) {
                $(this).scrollGallery({
                    sliderHolder: '.frame',
                    circleSlide: false,
                    step: true
                });
            } else if ($(this).find('li.productlist-item').length > 5) {
                $(this).scrollGallery({
                    sliderHolder: '.frame',
                    circleSlide: false,
                    step: true
                });
            } else {
                $(this).find('.link-prev,.link-next').hide();
            }
        });

        _holder.fadeGallery({
            slideElements: '.product-list-holder>.carousel',
            pagerLinks: '.carousel.first ul>li'
        })
    })
	
	//$('.frame ul li').removeClass('active');
	$('.frame ul li').removeAttr("class");
    $('.frame ul li').click(function () {
        $(this).addClass('active');
    });
}

function initBrochuresGallery() {
    $('.brochures-cont').each(function () {
        var _holder = $(this);
        var _gallery = $(this).find('.carousel-brochures');

        $(_gallery).each(function () {
            if ($(this).find('.brochures li').length > 3) {
                $(this).scrollGallery({
                    sliderHolder: '.frame',
                    circleSlide: false,
                    step: true
                });
                //$(this).find('.brochures li:nth-child(3n)').addClass('last');
            } else {
                $(this).find('.link-prev,.link-next').hide();
            }
        });

        _holder.fadeGallery({
            slideElements: '.carousel-brochures',
            pagerLinks: '.carousel-brochures ul>li'
        })
    })
}

function desaturateImages(){
	var _animSpeed = 500;
	$('.desaturate').each(function () {
	    var _holder = $(this);
	    var _img = $('img', _holder);
	    var _clone = _img.clone().insertAfter(_img);
	    _img.css({
	        'position': 'absolute',
	        'top': 0,
	        'left': 0,
	        'opacity': 0
	    });
	    var _fader = $('<div>').insertAfter(_img).css({
	        'position': 'absolute',
	        'top': 0,
	        'left': 0,
	        'width': _holder.width(),
	        'height': _holder.height(),
	        'background': '#ff0000',
	        'opacity': 0.5
	    });
	    if (_img.get(0).complete) {
	        _clone.grayscale();
	        var temp = 0;
	    }
	    else _img.get(0).onload = function () {
	        _clone.grayscale();
	    }
	    _holder.mouseenter(function () {
	        _img.stop().animate({
	            'opacity': 1
	        }, _animSpeed);
	        _fader.stop().animate({
	            'opacity': 0
	        }, _animSpeed);
	    }).mouseleave(function () {
	        _img.stop().animate({
	            'opacity': 0
	        }, _animSpeed);
	        _fader.stop().animate({
	            'opacity': 0.5
	        }, _animSpeed);
	    })
	})
}

//Project Item Desaturate function
//function desaturateProjectItemImages() {
//    var _animSpeed = 500;
//    $('.desaturate-project-item').each(function () {
//        var _holder = $(this);
//        var _img = $('img', _holder);
//        var _clone = _img.clone().insertAfter(_img);
//        _img.css({
//            'position': 'absolute',
//            'top': 0,
//            'left': 0,
//            'opacity': 0
//        });
//        if (_img.get(0).complete) _clone.grayscale();
//        else _img.get(0).onload = function () {
//            _clone.grayscale();
//        }
//        _holder.mouseenter(function () {
//            _img.stop().animate({
//                'opacity': 1
//            }, _animSpeed);
//            _fader.stop().animate({
//                'opacity': 0
//            }, _animSpeed);
//        }).mouseleave(function () {
//            _img.stop().animate({
//                'opacity': 0
//            }, _animSpeed);
//            _fader.stop().animate({
//                'opacity': 0.35
//            }, _animSpeed);
//        })
//    })
//}

/* Start Aundreas Code */

// ----  Desaturate  (Before Document Loads) ----------------------------------------------------------------------------------------------------------
var paircount = 0;

/* If you want to desaturate after page loaded - use onload event
* of image.
*/
function initImage(obj) {
    obj.onload = null;
    var $newthis = $(obj);
    if ($.browser.msie) {
        // You need this only if desaturate png with aplha channel
        $newthis = $newthis.desaturateImgFix();
    }
    // class for easy switch between color/gray version
    $newthis.addClass("pair_" + ++paircount);
    var $cloned = $newthis.clone().attr('id', '');
    // reset onload event on cloned object
    $cloned.get(0).onload = null;
    // add cloned after original image, we will switch between
    // original and cloned later
    $cloned.insertAfter($newthis).addClass("color").hide();
    // desaturate original
    $newthis = $newthis.desaturate();
    // add events for switch between color/gray versions
    $newthis.bind("mouseenter mouseleave", desevent);
    $cloned.bind("mouseenter mouseleave", desevent);
};

function desevent(event) {
    var classString = new String($(this).attr('class'));
    var pair = classString.match(/pair_\d+/);
    // first I try was $("."+pair).toggle() but IE switching very strange...
    $("." + pair).hide();
    if (event.type == 'mouseenter')
        $("." + pair).filter(".color").show().css({ 'display': 'block' });
    if (event.type == 'mouseleave')
        $("." + pair).filter(":not(.color)").show().css({ 'display': 'block' });
}

$(document).ready(function () {

    $('.desaturate_image').hover(function () {
        $(this).find('.overlay').fadeOut('normal');
        $(this).find('.des').hide();
        $(this).find('.des').filter(".color").show().css({ 'display': 'block' });

    }, function () {
        $(this).find('.overlay').fadeIn('fast');
        $(this).find('.des').filter(":not(.color)").show().css({ 'display': 'block' });
        $(this).find('.des').filter(".color").hide();
    });


});	

/* End Aundreas Code */



function initBrowserDetect() {
    var u = navigator.userAgent.toLowerCase();
    var _html = document.getElementsByTagName("html")[0];

    if (is("win")) addClass("win");
    else if (is("mac")) addClass("mac");
    else if (is("linux") || is("x11")) addClass("linux");

    if (is("msie 8.0")) addClass("ie8");
    else if (is("msie 7.0")) addClass("ie7");
    else if (is("msie 6.0")) addClass("ie6");
    else if (is("firefox/2")) addClass("ff2");
    else if (is("firefox/3")) addClass("ff3");
    else if (is("opera") && is("version/10")) addClass("opera10");
    else if (is("opera/9")) addClass("opera9");
    else if (is("safari") && is("version/3")) addClass("safari3");
    else if (is("safari") && is("version/4")) addClass("safari4");
    else if (is("safari") && is("version/5")) addClass("safari5");
    else if (is("chrome")) addClass("chrome");
    else if (is("safari")) addClass("safari2");
    else if (is("unknown")) addClass("unknown");

    if (is("msie")) addClass("trident");
    else if (is("applewebkit")) addClass("webkit");
    else if (is("gecko")) addClass("gecko");
    else if (is("opera")) addClass("presto");

    function is(browser) {
        if (u.indexOf(browser) != -1) return true;
    }
    function addClass(_class) {
        _html.className += (" " + _class);
    }
}

function initPage() {
    clearFormFields({
        clearInputs: true,
        clearTextareas: true,
        passwordFieldText: true,
        addClassFocus: "focus",
        filterClass: "default"
    });
}

function clearFormFields(o)
{
	if (o.clearInputs == null) o.clearInputs = true;
	if (o.clearTextareas == null) o.clearTextareas = true;
	if (o.passwordFieldText == null) o.passwordFieldText = false;
	if (o.addClassFocus == null) o.addClassFocus = false;
	if (!o.filterClass) o.filterClass = "default";

	function inputsSwap(el, el2) {
		if(el) el.style.display = "none";
		if(el2) el2.style.display = "inline";
	}
}

var VSA_scrollAreas = new Array();
var VSA_default_imagesPath = "images";
var VSA_default_btnUpImage = "button-up.gif";
var VSA_default_btnDownImage = "button-down.gif";

var VSA_default_scrollStep = 5;

var browserName = navigator.appName;
if (browserName == "Microsoft Internet Explorer") {
    VSA_default_scrollStep = 20;
}

var VSA_default_wheelSensitivity = 10;
var VSA_default_scrollbarPosition = 'right'; //'left','right','inline';
var VSA_default_scrollButtonHeight = 22;
var VSA_default_scrollbarWidth = 22;
var VSA_resizeTimer = 2000;
var VSA_touchFlag = isTouchDevice(); // true/false - move scroll with scrollable body

function VSA_initScrollbars() {
    if (!document.body.children) return;
    var scrollElements = VSA_getElements("vscrollable", "DIV", document, "class");
    for (var i = 0; i < scrollElements.length; i++) {
        VSA_scrollAreas[i] = new VScrollArea(i, scrollElements[i]);
    }
}

function isTouchDevice() {
    try {
        document.createEvent("TouchEvent");
        return true;
    } catch (e) {
        return false;
    }
}

function touchHandler(event) {
    var touches = event.changedTouches, first = touches[0], type = "";
    switch (event.type) {
        case "touchstart": type = "mousedown"; break;
        case "touchmove": type = "mousemove"; break;
        case "touchend": type = "mouseup"; break;
        default: return;
    }
    var simulatedEvent = document.createEvent("MouseEvent");
    simulatedEvent.initMouseEvent(type, true, true, window, 1, first.screenX, first.screenY, first.clientX, first.clientY, false, false, false, false, 0/*left*/, null);
    first.target.dispatchEvent(simulatedEvent);
    event.preventDefault();
}

function VScrollArea(index, elem) //constructor
{
    this.index = index;
    this.element = elem;

    var attr = this.element.getAttribute("imagesPath");
    this.imagesPath = attr ? attr : VSA_default_imagesPath;

    attr = this.element.getAttribute("btnUpImage");
    this.btnUpImage = attr ? attr : VSA_default_btnUpImage;

    attr = this.element.getAttribute("btnDownImage");
    this.btnDownImage = attr ? attr : VSA_default_btnDownImage;

    attr = Number(this.element.getAttribute("scrollStep"));
    this.scrollStep = attr ? attr : VSA_default_scrollStep;

    attr = Number(this.element.getAttribute("wheelSensitivity"));
    this.wheelSensitivity = attr ? attr : VSA_default_wheelSensitivity;

    attr = this.element.getAttribute("scrollbarPosition");
    this.scrollbarPosition = attr ? attr : VSA_default_scrollbarPosition;

    attr = this.element.getAttribute("scrollButtonHeight");
    this.scrollButtonHeight = attr ? attr : VSA_default_scrollButtonHeight;

    attr = this.element.getAttribute("scrollbarWidth");
    this.scrollbarWidth = attr ? attr : VSA_default_scrollbarWidth;

    this.scrolling = false;

    this.iOffsetY = 0;
    this.scrollHeight = 0;
    this.scrollContent = null;
    this.scrollbar = null;
    this.scrollup = null;
    this.scrolldown = null;
    this.scrollslider = null;
    this.scroll = null;
    this.enableScrollbar = false;
    this.scrollFactor = 1;
    this.scrollingLimit = 0;
    this.topPosition = 0;

    //functions declaration
    this.init = VSA_init;
    this.scrollUp = VSA_scrollUp;
    this.scrollDown = VSA_scrollDown;
    this.createScrollBar = VSA_createScrollBar;
    this.scrollIt = VSA_scrollIt;

    this.init();
}

function VSA_init() {
    this.scrollContent = document.createElement("DIV");
    this.scrollContent.style.position = "absolute";
    this.scrollContent.style.overflow = "hidden";
    this.scrollContent.style.width = this.element.offsetWidth + "px";
    this.scrollContent.style.height = this.element.offsetHeight + "px";

    while (this.element.childNodes.length) this.scrollContent.appendChild(this.element.childNodes[0]);

    this.element.style.overflow = "hidden";
    this.element.style.display = "block";
    this.element.style.visibility = "visible";
    this.element.style.position = "relative";
    this.element.appendChild(this.scrollContent);

    this.scrollContent.className = 'scroll-content';

    this.element.index = this.index;
    this.element.over = false;

    var _this = this;

    if (document.all && !window.opera) {
        this.element.onmouseenter = function () { _this.element.over = true; };
        this.element.onmouseleave = function () { _this.element.over = false; }
    } else {
        this.element.onmouseover = function () { _this.element.over = true; };
        this.element.onmouseout = function () { _this.element.over = false; }
    }

    if (document.all) {
        this.element.onscroll = VSA_handleOnScroll;
        this.element.onresize = VSA_handleResize;
    }
    else {
        window.onresize = VSA_handleResize;
    }

    this.createScrollBar();

    if (window.addEventListener) {
        /* DOMMouseScroll is for mozilla. */
        this.element.addEventListener('DOMMouseScroll', VSA_handleMouseWheel, false);
    }
    /* IE/Opera. */
    this.element.onmousewheel = document.onmousewheel = VSA_handleMouseWheel;

    // move content by touch
    if (VSA_touchFlag) {
        _this.scrollContent.onmousedown = function (e) {
            var startY = e.pageY - getRealTop(_this.scrollContent);
            var origTop = _this.scrollContent.scrollTop;
            _this.scrollContent.onmousemove = function (e) {
                var moveY = e.pageY - getRealTop(_this.scrollContent);
                var iNewY = origTop - (moveY - startY);
                if (iNewY < 0) iNewY = 0;
                if (iNewY > _this.scrollContent.scrollHeight) iNewY = _this.scrollContent.scrollHeight;
                _this.scrollContent.scrollTop = iNewY;
                _this.scrollslider.style.top = 1 / _this.scrollFactor * Math.abs(_this.scrollContent.scrollTop) + _this.scrollButtonHeight + "px";
            }
        }
        _this.scrollContent.onmouseup = function (e) {
            _this.scrollContent.onmousemove = null;
        }
        this.scrollContent.addEventListener("touchstart", touchHandler, true);
        this.scrollContent.addEventListener("touchmove", touchHandler, true);
        this.scrollContent.addEventListener("touchend", touchHandler, true);
    }
}

function VSA_createScrollBar() {
    if (this.scrollbar != null) {
        this.element.removeChild(this.scrollbar);
        this.scrollbar = null;
    }

    if (this.scrollContent.scrollHeight <= this.scrollContent.offsetHeight)
        this.enableScrollbar = false;
    else if (this.element.offsetHeight > 2 * this.scrollButtonHeight)
        this.enableScrollbar = true;
    else
        this.enableScrollbar = false;

    if (this.scrollContent.scrollHeight - Math.abs(this.scrollContent.scrollTop) < this.element.offsetHeight)
        this.scrollContent.style.top = 0;

    if (this.enableScrollbar) {
        this.scrollbar = document.createElement("DIV");
        this.element.appendChild(this.scrollbar);
        this.scrollbar.style.position = "absolute";
        this.scrollbar.style.top = "0px";
        this.scrollbar.style.height = this.element.offsetHeight + "px";
        this.scrollbar.style.width = this.scrollbarWidth + "px";

        this.scrollbar.className = 'vscroll-bar';

        if (this.scrollbarWidth != this.scrollbar.offsetWidth) {
            this.scrollbarWidth = this.scrollbar.offsetHeight;
        }

        this.scrollbarWidth = this.scrollbar.offsetWidth;

        if (this.scrollbarPosition == 'left') {
            this.scrollContent.style.left = this.scrollbarWidth + 5 + "px";
            this.scrollContent.style.width = this.element.offsetWidth - this.scrollbarWidth - 5 + "px";
        }
        else if (this.scrollbarPosition == 'right') {
            this.scrollbar.style.left = this.element.offsetWidth - this.scrollbarWidth + "px";
            this.scrollContent.style.width = this.element.offsetWidth - this.scrollbarWidth - 5 + "px";
        }

        //create scroll up button
        this.scrollup = document.createElement("DIV");
        this.scrollup.index = this.index;
        this.scrollup.onmousedown = VSA_handleBtnUpMouseDown;
        this.scrollup.onmouseup = VSA_handleBtnUpMouseUp;
        this.scrollup.onmouseout = VSA_handleBtnUpMouseOut;

        if (VSA_touchFlag) {
            this.scrollup.addEventListener("touchstart", touchHandler, true);
            this.scrollup.addEventListener("touchend", touchHandler, true);
        }

        this.scrollup.style.position = "absolute";
        this.scrollup.style.top = "0px";
        this.scrollup.style.left = "0px";
        this.scrollup.style.height = this.scrollButtonHeight + "px";
        this.scrollup.style.width = this.scrollbarWidth + "px";

        this.scrollup.innerHTML = '<img src="' + this.imagesPath + '/' + this.btnUpImage + '" border="0"/>';
        this.scrollbar.appendChild(this.scrollup);

        this.scrollup.className = 'vscroll-up';

        if (this.scrollButtonHeight != this.scrollup.offsetHeight) {
            this.scrollButtonHeight = this.scrollup.offsetHeight;
        }

        //create scroll down button
        this.scrolldown = document.createElement("DIV");
        this.scrolldown.index = this.index;
        this.scrolldown.onmousedown = VSA_handleBtnDownMouseDown;
        this.scrolldown.onmouseup = VSA_handleBtnDownMouseUp;
        this.scrolldown.onmouseout = VSA_handleBtnDownMouseOut;

        if (VSA_touchFlag) {
            this.scrolldown.addEventListener("touchstart", touchHandler, true);
            this.scrolldown.addEventListener("touchend", touchHandler, true);
        }

        this.scrolldown.style.position = "absolute";
        this.scrolldown.style.left = "0px";
        this.scrolldown.style.top = this.scrollbar.offsetHeight - this.scrollButtonHeight + "px";
        this.scrolldown.style.width = this.scrollbarWidth + "px";
        this.scrolldown.innerHTML = '<img src="' + this.imagesPath + '/' + this.btnDownImage + '" border="0"/>';
        this.scrollbar.appendChild(this.scrolldown);

        this.scrolldown.className = 'vscroll-down';

        //create scroll
        this.scroll = document.createElement("DIV");
        this.scroll.index = this.index;
        this.scroll.style.position = "absolute";
        this.scroll.style.zIndex = 0;
        this.scroll.style.textAlign = "center";
        this.scroll.style.top = this.scrollButtonHeight + "px";
        this.scroll.style.left = "0px";
        this.scroll.style.width = this.scrollbarWidth + "px";

        var h = this.scrollbar.offsetHeight - 2 * this.scrollButtonHeight;
        this.scroll.style.height = ((h > 0) ? h : 0) + "px";

        this.scroll.innerHTML = '';
        this.scroll.onclick = VSA_handleScrollbarClick;
        this.scrollbar.appendChild(this.scroll);
        this.scroll.style.overflow = "hidden";

        this.scroll.className = "vscroll-line";

        //create slider
        this.scrollslider = document.createElement("DIV");
        this.scrollslider.index = this.index;
        this.scrollslider.style.position = "absolute";
        this.scrollslider.style.zIndex = 1000;
        this.scrollslider.style.textAlign = "center";
        this.scrollslider.innerHTML = '<div id="scrollslider' + this.index + '" style="padding:0;margin:0;"><div class="scroll-bar-top"></div><div class="scroll-bar-bottom"></div></div>';
        this.scrollbar.appendChild(this.scrollslider);

        this.subscrollslider = document.getElementById("scrollslider" + this.index);
        this.subscrollslider.style.height = Math.round((this.scrollContent.offsetHeight / this.scrollContent.scrollHeight) * (this.scrollbar.offsetHeight - 2 * this.scrollButtonHeight)) + "px";

        this.scrollslider.className = "vscroll-slider";

        this.scrollHeight = this.scrollbar.offsetHeight - 2 * this.scrollButtonHeight - this.scrollslider.offsetHeight;
        this.scrollFactor = (this.scrollContent.scrollHeight - this.scrollContent.offsetHeight) / this.scrollHeight;
        this.topPosition = getRealTop(this.scrollbar) + this.scrollButtonHeight;
        /* this.scrollbarHeight = this.scrollbar.offsetHeight - 2*this.scrollButtonHeight - this.scrollslider.offsetHeight; */

        this.scrollslider.style.top = /* 1 / this.scrollFactor * Math.abs(this.scrollContent.offsetTop) +*/this.scrollButtonHeight + "px";
        this.scrollslider.style.left = "0px";
        this.scrollslider.style.width = "100%";
        this.scrollslider.onmousedown = VSA_handleSliderMouseDown;
        if (VSA_touchFlag) {
            this.scrollslider.addEventListener("touchstart", touchHandler, true);
        }
        if (document.all)
            this.scrollslider.onmouseup = VSA_handleSliderMouseUp;
    }
    else
        this.scrollContent.style.width = this.element.offsetWidth + "px";
}

function VSA_handleBtnUpMouseDown() {
    var sa = VSA_scrollAreas[this.index];
    sa.scrolling = true;
    sa.scrollUp();
}

function VSA_handleBtnUpMouseUp() {
    VSA_scrollAreas[this.index].scrolling = false;
}

function VSA_handleBtnUpMouseOut() {
    VSA_scrollAreas[this.index].scrolling = false;
}

function VSA_handleBtnDownMouseDown() {
    var sa = VSA_scrollAreas[this.index];
    sa.scrolling = true;
    sa.scrollDown();
}

function VSA_handleBtnDownMouseUp() {
    VSA_scrollAreas[this.index].scrolling = false;
}

function VSA_handleBtnDownMouseOut() {
    VSA_scrollAreas[this.index].scrolling = false;
}

function VSA_scrollIt() {
    this.scrollContent.scrollTop = this.scrollFactor * ((this.scrollslider.offsetTop + this.scrollslider.offsetHeight / 2) - this.scrollButtonHeight - this.scrollslider.offsetHeight / 2);
}

function VSA_scrollUp() {
    if (this.scrollingLimit > 0) {
        this.scrollingLimit--;
        if (this.scrollingLimit == 0) this.scrolling = false;
    }
    if (!this.scrolling) return;
    if (this.scrollContent.scrollTop - this.scrollStep > 0) {
        this.scrollContent.scrollTop -= this.scrollStep;
        this.scrollslider.style.top = 1 / this.scrollFactor * Math.abs(this.scrollContent.scrollTop) + this.scrollButtonHeight + "px";
    }
    else {
        this.scrollContent.scrollTop = "0";
        this.scrollslider.style.top = this.scrollButtonHeight + "px";
        return;
    }
    setTimeout("VSA_Ext_scrollUp(" + this.index + ")", 30);
}

function VSA_Ext_scrollUp(index) {
    VSA_scrollAreas[index].scrollUp();
}

function VSA_scrollDown() {
    if (this.scrollingLimit > 0) {
        this.scrollingLimit--;
        if (this.scrollingLimit == 0) this.scrolling = false;
    }
    if (!this.scrolling) return;


    this.scrollContent.scrollTop += this.scrollStep;
    this.scrollslider.style.top = 1 / this.scrollFactor * Math.abs(this.scrollContent.scrollTop) + this.scrollButtonHeight + "px";

    if (this.scrollContent.scrollTop >= (this.scrollContent.scrollHeight - this.scrollContent.offsetHeight)) {
        this.scrollContent.scrollTop = (this.scrollContent.scrollHeight - this.scrollContent.offsetHeight);
        this.scrollslider.style.top = this.scrollbar.offsetHeight - this.scrollButtonHeight - this.scrollslider.offsetHeight + "px";
        return;
    }

    setTimeout("VSA_Ext_scrollDown(" + this.index + ")", 30);
}

function VSA_Ext_scrollDown(index) {
    VSA_scrollAreas[index].scrollDown();
}

function VSA_handleMouseMove(evt) {
    var sa = VSA_scrollAreas[((document.all && !window.opera) ? this.index : document.documentElement.scrollAreaIndex)];
    var posy = 0;
    if (!evt) var evt = window.event;

    if (evt.pageY)
        posy = evt.pageY;
    else if (evt.clientY)
        posy = evt.clientY;

    if (document.all && !window.opera) {
        if (!document.addEventListener) {
            posy += document.documentElement.scrollTop;
        }
    }

    var iNewY = posy - sa.iOffsetY - getRealTop(sa.scrollbar) - sa.scrollButtonHeight;
    iNewY += sa.scrollButtonHeight;

    if (iNewY < sa.scrollButtonHeight)
        iNewY = sa.scrollButtonHeight;
    if (iNewY > (sa.scrollbar.offsetHeight - sa.scrollButtonHeight) - sa.scrollslider.offsetHeight)
        iNewY = (sa.scrollbar.offsetHeight - sa.scrollButtonHeight) - sa.scrollslider.offsetHeight;

    sa.scrollslider.style.top = iNewY + "px";

    sa.scrollIt();
}

function VSA_handleSliderMouseDown(evt) {
    if (!(document.uniqueID && document.compatMode && !window.XMLHttpRequest)) {
        document.onselectstart = function () { return false; }
        document.onmousedown = function () { return false; }
    }

    var sa = VSA_scrollAreas[this.index];
    if (document.all && !window.opera) {
        sa.scrollslider.setCapture()
        sa.iOffsetY = event.offsetY;
        sa.scrollslider.onmousemove = VSA_handleMouseMove;
        if (VSA_touchFlag) {
            sa.scrollslider.addEventListener("touchmove", touchHandler, true);
        }
    }
    else {
        if (window.opera) {
            sa.iOffsetY = event.offsetY;
        }
        else {
            sa.iOffsetY = evt.layerY;
        }
        document.documentElement.scrollAreaIndex = sa.index;
        document.documentElement.addEventListener("mousemove", VSA_handleMouseMove, true);
        document.documentElement.addEventListener("mouseup", VSA_handleSliderMouseUp, true);
        if (VSA_touchFlag) {
            document.documentElement.addEventListener("touchmove", touchHandler, true);
            document.documentElement.addEventListener("touchend", touchHandler, true);
        }
    }
    return false;
}

function VSA_handleSliderMouseUp() {
    if (!(document.uniqueID && document.compatMode && !window.XMLHttpRequest)) {
        document.onmousedown = null;
        document.onselectstart = null;
    }

    if (document.all && !window.opera) {
        var sa = VSA_scrollAreas[this.index];
        sa.scrollslider.onmousemove = null;
        sa.scrollslider.releaseCapture();
        sa.scrollIt();
    }
    else {
        var sa = VSA_scrollAreas[document.documentElement.scrollAreaIndex];
        document.documentElement.removeEventListener("mousemove", VSA_handleMouseMove, true);
        document.documentElement.removeEventListener("mouseup", VSA_handleSliderMouseUp, true);
        if (VSA_touchFlag) {
            document.documentElement.removeEventListener("touchmove", touchHandler, true);
            document.documentElement.removeEventListener("touchend", touchHandler, true);
        }
        sa.scrollIt();
    }
    return false;
}

function VSA_handleResize() {
    if (VSA_resizeTimer) {
        clearTimeout(VSA_resizeTimer);
        VSA_resizeTimer = 0;
    }
    VSA_resizeTimer = setTimeout("VSA_performResizeEvent()", 100);
}

function VSA_performResizeEvent() {
    for (var i = 0; i < VSA_scrollAreas.length; i++)
        VSA_scrollAreas[i].createScrollBar();
}
function VSA_handleMouseWheel(event) {
    if (this.index != null) {
        var sa = VSA_scrollAreas[this.index];
        if (sa.scrollbar == null) return;
        sa.scrolling = true;
        sa.scrollingLimit = sa.wheelSensitivity;

        var delta = 0;
        if (!event) /* For IE. */
            event = window.event;
        if (event.wheelDelta) { /* IE/Opera. */
            delta = event.wheelDelta / 120;
            /*if (window.opera) delta = -delta;*/
        } else if (event.detail) { /* Mozilla case. */
            delta = -event.detail / 3;
        }

        if (delta && sa.element.over) {
            if (delta > 0) {
                sa.scrollUp();
            } else {
                sa.scrollDown();
            }
            if (event.preventDefault) {
                event.preventDefault();
            }
            event.returnValue = false;
        }
    }
}

function VSA_handleSelectStart() {
    event.returnValue = false;
}

function VSA_handleScrollbarClick(evt) {
    var sa = VSA_scrollAreas[this.index];
    var offsetY = (document.all ? event.offsetY : evt.layerY);

    if (offsetY < (sa.scrollButtonHeight + sa.scrollslider.offsetHeight / 2))
        sa.scrollslider.style.top = sa.scrollButtonHeight + "px";
    else if (offsetY > (sa.scrollbar.offsetHeight - sa.scrollButtonHeight - sa.scrollslider.offsetHeight))
        sa.scrollslider.style.top = sa.scrollbar.offsetHeight - sa.scrollButtonHeight - sa.scrollslider.offsetHeight + "px";
    else {
        sa.scrollslider.style.top = offsetY + sa.scrollButtonHeight - sa.scrollslider.offsetHeight / 2 + "px";
    }
    sa.scrollIt();
}

function VSA_handleOnScroll() {
    //event.srcElement.doScroll("pageUp");
}

//--- common functions ----

function VSA_getElements(attrValue, tagName, ownerNode, attrName) //get Elements By Attribute Name
{
    if (!tagName) tagName = "*";
    if (!ownerNode) ownerNode = document;
    if (!attrName) attrName = "name";
    var result = [];
    var nl = ownerNode.getElementsByTagName(tagName);
    for (var i = 0; i < nl.length; i++) {
        //	if (nl.item(i).getAttribute(attrName) == attrValue)
        //		result.push(nl.item(i));
        if (nl.item(i).className.indexOf(attrValue) != -1)
            result.push(nl.item(i));
    }
    return result;
}

function getRealTop(obj) {
    var posTop = 0;
    while (obj.offsetParent) {
        posTop += obj.offsetTop;
        obj = obj.offsetParent;
    }
    return posTop;
}

/**
* Image Desaturate - jQuery plugin
* Desaturate (convert) all types of images on web page
* 
* (c) 2010 Dmitry Kelmi <miksir@maker.ru>
* Version: 0.5.2 (04 Aug 2010)
* Requires: jQuery v1.3+
* 
* Dual licensed under the MIT and GPL licenses:
*   http://www.opensource.org/licenses/mit-license.php
*   http://www.gnu.org/licenses/gpl.html
* 
* How to use:
*  $(selector).desaturate(options);
*  options = {
*    'iefix': true or false  - autofix images for IE(6-8)
*  }
*
*  IE fix need in following cases:
*    - image is png with transparency - it apply standart IE fix for png
*    - desaturated image will be switched with opacity (fadeIn/fadeOut) - jQuery can reset filters after
*      fadeIn / fadeOut and lost desaturate effect
*  'iefix' apply fixes only to target of desaturate() and not to other png images!
*  If you set 'iefix' to false, you can fix images with .desaturateImgFix(), note: all other kind of scripts
*  for PNG IE fix wont work with .desaturate().
*
*  Note: desaturate will replace current image and return new node as result (for all browsers)
*  Note for non-ie: u can't desaturate IMG which is not loaded yet, so better to use it with onload event.
*/
jQuery.desaturate = {
    defaults: {
        'iefix': true, // autofix png for IE
        'level': 1,    // level of desaturation, ignored in IE
        'rgb': [0.3333, 0.3333, 0.3333] // levels of RGB for compose grayscale, ignored in IE
    },
    customClass: 'js-desaturate-fixed' // usually no need to change this
};

jQuery.desaturate.Image = function (obj) {
    this.image = obj;
    this.jImage = jQuery(this.image);

    this.src = this.jImage.attr('src');
    this.isPNG = this.jImage.is("IMG[src$=.png]");

    var styleWidth = new String(this.jImage.css('width')); styleWidth = styleWidth.replace(/px/, '');
    var styleHeight = new String(this.jImage.css('height')); styleHeight = styleHeight.replace(/px/, '');

    this.width = this.jImage.width() ? this.jImage.width() : (styleWidth ? styleWidth : this.jImage.attr('width'));
    this.height = this.jImage.height() ? this.jImage.height() : (styleHeight ? styleHeight : this.jImage.attr('height'));

    //      var styles = ['padding', 'margin', 'border'];
    //      for (var i in styles) {
    //        this.imgCustomStyles += styles[i] + ':' + this.image.style[styles[i]]+';';
    //        this.image.style[styles[i]] = '';
    //      }

    this.imgFilter = '';
    if (this.image.style.filter) {
        this.imgFilter = 'filter:' + this.image.style.filter + ';';
        this.image.style.filter = '';
    }

    this.image.style.width = '';
    this.image.style.height = '';

    this.imgId = this.jImage.attr('id') ? 'id="' + this.jImage.attr('id') + '" ' : '';
    this.imgClass = 'class="' + this.jImage.attr('class') + ' ' + jQuery.desaturate.customClass + '" ';
    this.imgTitle = this.jImage.attr('title') ? 'title="' + this.jImage.attr('title') + '" ' : '';
    this.imgAlt = this.jImage.attr('alt') ? 'alt="' + this.jImage.attr('alt') + '" ' : '';

    this.imgStyles = this.image.style.cssText;
    this.imgStyles += this.jImage.attr('align') ? 'float:' + this.jImage.attr('align') + ';' : '';
    this.imgStyles += this.jImage.parent().attr('href') ? 'cursor:hand;' : '';

    // nulled filter present as FILTER: in cssText
    this.imgStyles = this.imgStyles.replace(/filter:/i, '');


    this.imgCssSize = (this.width && this.height) ? 'width:' + this.width + 'px;' + 'height:' + this.height + 'px;' : '';
};

jQuery.desaturate.Image.prototype.replace = function (html) {
    return jQuery(html).replaceAll(this.image).get(0);
};

jQuery.desaturate.Image.prototype.getCanvas = function (options) {
    var canvasStr = '<canvas style="display:inline-block;' + this.imgStyles + this.imgCssSize + '" ';
    canvasStr += this.imgId + this.imgClass + this.imgTitle + this.imgAlt + '></canvas>';

    var canvas = jQuery(canvasStr).get(0);
    var canvasContext = canvas.getContext('2d');

    var imgW = this.width;
    var imgH = this.height;
    canvas.width = imgW;
    canvas.height = imgH;

    canvasContext.drawImage(this.image, 0, 0);

    var imgPixels = canvasContext.getImageData(0, 0, imgW, imgH);

    for (var y = 0; y < imgPixels.height; y++) {
        for (var x = 0; x < imgPixels.width; x++) {
            var i = (y * 4) * imgPixels.width + x * 4;
            var avg = imgPixels.data[i] * options.rgb[0] + imgPixels.data[i + 1] * options.rgb[1] + imgPixels.data[i + 2] * options.rgb[2];
            imgPixels.data[i] = avg * options.level + imgPixels.data[i] * (1 - options.level);
            imgPixels.data[i + 1] = avg * options.level + imgPixels.data[i + 1] * (1 - options.level);
            imgPixels.data[i + 2] = avg * options.level + imgPixels.data[i + 2] * (1 - options.level);
        }
    }

    canvasContext.putImageData(imgPixels, 0, 0, 0, 0, imgPixels.width, imgPixels.height);
    return canvas;
}

jQuery.desaturate.Image.prototype.getIeFix = function (options) {
    /* Some jQuery operations like fadeIn/Out can reset filter atribute, so we need 3 SPAN's: 1st for styles and
    * correct work with jQuery's animation, 2rd for grayScale filter and last one for alpha image filter.
    * Combined 2 filters in one span won't work too.
    */
    var blockInit = 'display:block;background:transparent;padding:0;margin:0;';
    var strNewHTML = '<span style="display:inline-block;' + this.imgStyles + this.imgCssSize + '" ';
    strNewHTML += this.imgId + this.imgClass + this.imgTitle + this.imgAlt + '>';
    strNewHTML += '<span style="' + blockInit + this.imgCssSize + this.imgFilter + '">';
    if (this.isPNG) {
        strNewHTML += '<span style="' + blockInit + this.imgCssSize;
        strNewHTML += 'filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src=\'' + this.src + '\', sizingMethod=\'crop\');">';
        strNewHTML += '</span>';
    } else {
        strNewHTML += '<img style="' + blockInit + this.imgCssSize + '" ' + this.imgTitle + this.imgAlt;
        strNewHTML += ' src="' + this.src + '">';
    }
    strNewHTML += '</span>';
    strNewHTML += '</span>';

    return jQuery(strNewHTML).get(0);
}

jQuery.fn.desaturate = function (options) {

    var ret = [];
    var _opt = jQuery.extend(true, {}, jQuery.desaturate.defaults, options);

    this.each(function () {
        var el = this;
        var $opt = jQuery.extend(true, {}, _opt, jQuery.metadata ? jQuery(el).metadata() : {}, jQuery(el).data('desaturate'));

        if (jQuery.browser.msie && jQuery(el).is("IMG") && $opt.iefix) {
            // autofix IE images
            var image = new jQuery.desaturate.Image(el);
            el = image.replace(image.getIeFix($opt));
        }

        if (jQuery.browser.msie && (jQuery(el).is("IMG") || jQuery(el).hasClass(jQuery.desaturate.customClass))) {
            // apply filter for IE
            var el1 = el;
            if (jQuery(el).hasClass(jQuery.desaturate.customClass)) {
                // if this element is our imgage fixed by pngIE - set grayscale filter to child span
                el1 = jQuery("SPAN", el).get(0);
            }
            el1.style.filter = (el1.style.filter ? el1.style.filter + ' ' : '') +
                            'progid:DXImageTransform.Microsoft.BasicImage(grayScale=1)';
        }

        if (!jQuery.browser.msie && (jQuery(el).is("IMG"))) {
            // convert image to canvas
            var image = new jQuery.desaturate.Image(el);
            el = image.replace(image.getCanvas($opt));
        }

        ret.push(el);
    });

    return this.pushStack(ret, "desaturate", "");
};

jQuery.fn.desaturateImgFix = function (options) {
    if (!jQuery.browser.msie) {
        return this;
    }

    var _opt = jQuery.extend(true, {}, jQuery.desaturate.defaults, options);
    var ret = [];

    this.each(function () {
        var $opt = jQuery.extend(true, {}, _opt, jQuery.metadata ? jQuery(this).metadata() : {}, jQuery(this).data('desaturate'));
        if (!jQuery(this).is("IMG")) {
            ret.push(this);
        } else {
            var image = new jQuery.desaturate.Image(this);
            ret.push(image.replace(image.getIeFix($opt)));
        }
    });

    return this.pushStack(ret, "desaturateImgFix", "");
};


// slideshow plugin
jQuery.fn.fadeGallery = function (_options) {
    var _options = jQuery.extend({
        slideElements: 'div.slideset > div',
        pagerLinks: 'div.pager a',
        btnNext: 'a.next',
        btnPrev: 'a.prev',
        btnPlayPause: 'a.play-pause',
        btnPlay: 'a.play',
        btnPause: 'a.pause',
        pausedClass: 'paused',
        disabledClass: 'disabled',
        playClass: 'playing',
        activeClass: 'active',
        loadingClass: 'ajax-loading',
        loadedClass: 'slide-loaded',
        dynamicImageLoad: false,
        dynamicImageLoadAttr: 'alt',
        currentNum: false,
        allNum: false,
        startSlide: null,
        noCircle: false,
        pauseOnHover: true,
        autoRotation: false,
        autoHeight: false,
        onBeforeFade: false,
        onAfterFade: false,
        onChange: false,
        disableWhileAnimating: false,
        switchTime: 3000,
        duration: 650,
        event: 'click'
    }, _options);

    return this.each(function () {
        // gallery options
        if (this.slideshowInit) return; else this.slideshowInit;
        var _this = jQuery(this);
        var _slides = jQuery(_options.slideElements, _this);
        var _pagerLinks = jQuery(_options.pagerLinks, _this);
        var _btnPrev = jQuery(_options.btnPrev, _this);
        var _btnNext = jQuery(_options.btnNext, _this);
        var _btnPlayPause = jQuery(_options.btnPlayPause, _this);
        var _btnPause = jQuery(_options.btnPause, _this);
        var _btnPlay = jQuery(_options.btnPlay, _this);
        var _pauseOnHover = _options.pauseOnHover;
        var _dynamicImageLoad = _options.dynamicImageLoad;
        var _dynamicImageLoadAttr = _options.dynamicImageLoadAttr;
        var _autoRotation = _options.autoRotation;
        var _activeClass = _options.activeClass;
        var _loadingClass = _options.loadingClass;
        var _loadedClass = _options.loadedClass;
        var _disabledClass = _options.disabledClass;
        var _pausedClass = _options.pausedClass;
        var _playClass = _options.playClass;
        var _autoHeight = _options.autoHeight;
        var _duration = _options.duration;
        var _switchTime = _options.switchTime;
        var _controlEvent = _options.event;
        var _currentNum = (_options.currentNum ? jQuery(_options.currentNum, _this) : false);
        var _allNum = (_options.allNum ? jQuery(_options.allNum, _this) : false);
        var _startSlide = _options.startSlide;
        var _noCycle = _options.noCircle;
        var _onChange = _options.onChange;
        var _onBeforeFade = _options.onBeforeFade;
        var _onAfterFade = _options.onAfterFade;
        var _disableWhileAnimating = _options.disableWhileAnimating;

        // gallery init
        var _anim = false;
        var _hover = false;
        var _prevIndex = 0;
        var _currentIndex = 0;
        var _slideCount = _slides.length;
        var _timer;
        if (_slideCount < 2) return;

        _prevIndex = _slides.index(_slides.filter('.' + _activeClass));
        if (_prevIndex < 0) _prevIndex = _currentIndex = 0;
        else _currentIndex = _prevIndex;
        if (_startSlide != null) {
            if (_startSlide == 'random') _prevIndex = _currentIndex = Math.floor(Math.random() * _slideCount);
            else _prevIndex = _currentIndex = parseInt(_startSlide);
        }
        _slides.hide().eq(_currentIndex).show();
        if (_autoRotation) _this.removeClass(_pausedClass).addClass(_playClass);
        else _this.removeClass(_playClass).addClass(_pausedClass);

        // gallery control
        if (_btnPrev.length) {
            _btnPrev.bind(_controlEvent, function () {
                prevSlide();
                return false;
            });
        }
        if (_btnNext.length) {
            _btnNext.bind(_controlEvent, function () {
                nextSlide();
                return false;
            });
        }
        if (_pagerLinks.length) {
            _pagerLinks.each(function (_ind) {
                jQuery(this).bind(_controlEvent, function () {
                    if (_currentIndex != _ind) {
                        if (_disableWhileAnimating && _anim) return;
                        _prevIndex = _currentIndex;
                        _currentIndex = _ind;
                        switchSlide();
                    }
                    return false;
                });
            });
        }

        // play pause section
        if (_btnPlayPause.length) {
            _btnPlayPause.bind(_controlEvent, function () {
                if (_this.hasClass(_pausedClass)) {
                    _this.removeClass(_pausedClass).addClass(_playClass);
                    _autoRotation = true;
                    autoSlide();
                } else {
                    _autoRotation = false;
                    if (_timer) clearTimeout(_timer);
                    _this.removeClass(_playClass).addClass(_pausedClass);
                }
                return false;
            });
        }
        if (_btnPlay.length) {
            _btnPlay.bind(_controlEvent, function () {
                _this.removeClass(_pausedClass).addClass(_playClass);
                _autoRotation = true;
                autoSlide();
                return false;
            });
        }
        if (_btnPause.length) {
            _btnPause.bind(_controlEvent, function () {
                _autoRotation = false;
                if (_timer) clearTimeout(_timer);
                _this.removeClass(_playClass).addClass(_pausedClass);
                return false;
            });
        }

        // dynamic image loading (swap from ATTRIBUTE)
        function loadSlide(slide) {
            if (!slide.hasClass(_loadingClass) && !slide.hasClass(_loadedClass)) {
                var images = slide.find(_dynamicImageLoad) // pass selector here
                var imagesCount = images.length;
                if (imagesCount) {
                    slide.addClass(_loadingClass);
                    images.each(function () {
                        var img = this;
                        img.onload = function () {
                            img.loaded = true;
                            img.onload = null;
                            setTimeout(reCalc, _duration);
                        }
                        img.setAttribute('src', img.getAttribute(_dynamicImageLoadAttr));
                        img.setAttribute(_dynamicImageLoadAttr, '');
                    }).css({ opacity: 0 });

                    function reCalc() {
                        var cnt = 0;
                        images.each(function () {
                            if (this.loaded) cnt++;
                        });
                        if (cnt == imagesCount) {
                            slide.removeClass(_loadingClass);
                            images.animate({ opacity: 1 }, { duration: _duration, complete: function () {
                                if (jQuery.browser.msie && jQuery.browser.version < 9) jQuery(this).css({ opacity: 'auto' })
                            } 
                            });
                            slide.addClass(_loadedClass)
                        }
                    }
                }
            }
        }

        // gallery animation
        function prevSlide() {
            if (_disableWhileAnimating && _anim) return;
            _prevIndex = _currentIndex;
            if (_currentIndex > 0) _currentIndex--;
            else {
                if (_noCycle) return;
                else _currentIndex = _slideCount - 1;
            }
            switchSlide();
        }
        function nextSlide() {
            if (_disableWhileAnimating && _anim) return;
            _prevIndex = _currentIndex;
            if (_currentIndex < _slideCount - 1) _currentIndex++;
            else {
                if (_noCycle) return;
                else _currentIndex = 0;
            }
            switchSlide();
        }
        function refreshStatus() {
            if (_dynamicImageLoad) loadSlide(_slides.eq(_currentIndex));
            if (_pagerLinks.length) _pagerLinks.removeClass(_activeClass).eq(_currentIndex).addClass(_activeClass);
            if (_currentNum) _currentNum.text(_currentIndex + 1);
            if (_allNum) _allNum.text(_slideCount);
            _slides.eq(_prevIndex).removeClass(_activeClass);
            _slides.eq(_currentIndex).addClass(_activeClass);
            if (_noCycle) {
                if (_btnPrev.length) {
                    if (_currentIndex == 0) _btnPrev.addClass(_disabledClass);
                    else _btnPrev.removeClass(_disabledClass);
                }
                if (_btnNext.length) {
                    if (_currentIndex == _slideCount - 1) _btnNext.addClass(_disabledClass);
                    else _btnNext.removeClass(_disabledClass);
                }
            }
            if (typeof _onChange === 'function') {
                _onChange(_this, _slides, _prevIndex, _currentIndex);
            }
        }
        function switchSlide() {
            _anim = true;
            if (typeof _onBeforeFade === 'function') _onBeforeFade(_this, _slides, _prevIndex, _currentIndex);
            _slides.eq(_prevIndex).fadeOut(_duration, function () {
                _anim = false;
            });
            _slides.eq(_currentIndex).fadeIn(_duration, function () {
                if (typeof _onAfterFade === 'function') _onAfterFade(_this, _slides, _prevIndex, _currentIndex);
            });
            if (_autoHeight) _slides.eq(_currentIndex).parent().animate({ height: _slides.eq(_currentIndex).outerHeight(true) }, { duration: _duration, queue: false });
            refreshStatus();
            autoSlide();
        }

        // autoslide function
        function autoSlide() {
            if (!_autoRotation || _hover) return;
            if (_timer) clearTimeout(_timer);
            _timer = setTimeout(nextSlide, _switchTime + _duration);
        }
        if (_pauseOnHover) {
            _this.hover(function () {
                _hover = true;
                if (_timer) clearTimeout(_timer);
            }, function () {
                _hover = false;
                autoSlide();
            });
        }
        refreshStatus();
        autoSlide();
    });
}

// scrolling gallery plugin
jQuery.fn.scrollGallery = function (_options) {
    var _options = jQuery.extend({
        sliderHolder: '>div',
        slider: '>ul',
        slides: '>li',
        pagerLinks: 'div.pager a',
        btnPrev: 'a.link-prev, .prev',
        btnNext: 'a.link-next, .next',
        activeClass: 'active',
        disabledClass: 'disabled',
        generatePagination: 'div.pg-holder',
        curNum: 'em.scur-num',
        allNum: 'em.sall-num',
        circleSlide: true,
        pauseClass: 'gallery-paused',
        pauseButton: 'none',
        pauseOnHover: true,
        autoRotation: false,
        stopAfterClick: false,
        switchTime: 5000,
        duration: 650,
        easing: 'swing',
        event: 'click',
        splitCount: false,
        afterInit: false,
        vertical: false,
        step: false
    }, _options);

    return this.each(function () {
        // gallery options
        var _this = jQuery(this);
        var _sliderHolder = jQuery(_options.sliderHolder, _this);
        var _slider = jQuery(_options.slider, _sliderHolder);
        var _slides = jQuery(_options.slides, _slider);
        var _btnPrev = jQuery(_options.btnPrev, _this);
        var _btnNext = jQuery(_options.btnNext, _this);
        var _pagerLinks = jQuery(_options.pagerLinks, _this);
        var _generatePagination = jQuery(_options.generatePagination, _this);
        var _curNum = jQuery(_options.curNum, _this);
        var _allNum = jQuery(_options.allNum, _this);
        var _pauseButton = jQuery(_options.pauseButton, _this);
        var _pauseOnHover = _options.pauseOnHover;
        var _pauseClass = _options.pauseClass;
        var _autoRotation = _options.autoRotation;
        var _activeClass = _options.activeClass;
        var _disabledClass = _options.disabledClass;
        var _easing = _options.easing;
        var _duration = _options.duration;
        var _switchTime = _options.switchTime;
        var _controlEvent = _options.event;
        var _step = _options.step;
        var _vertical = _options.vertical;
        var _circleSlide = _options.circleSlide;
        var _stopAfterClick = _options.stopAfterClick;
        var _afterInit = _options.afterInit;
        var _splitCount = _options.splitCount;

        // gallery init
        if (!_slides.length) return;

        if (_splitCount) {
            var curStep = 0;
            var newSlide = $('<slide>').addClass('split-slide');
            _slides.each(function () {
                newSlide.append(this);
                curStep++;
                if (curStep > _splitCount - 1) {
                    curStep = 0;
                    _slider.append(newSlide);
                    newSlide = $('<slide>').addClass('split-slide');
                }
            });
            if (curStep) _slider.append(newSlide);
            _slides = _slider.children();
        }

        var _currentStep = 0;
        var _sumWidth = 0;
        var _sumHeight = 0;
        var _hover = false;
        var _stepWidth;
        var _stepHeight;
        var _stepCount;
        var _offset;
        var _timer;

        _slides.each(function () {
            _sumWidth += $(this).outerWidth(true);
            _sumHeight += $(this).outerHeight(true);
        });

        // calculate gallery offset
        function recalcOffsets() {
            if (_vertical) {
                if (_step) {
                    _stepHeight = _slides.eq(_currentStep).outerHeight(true);
                    _stepCount = Math.ceil((_sumHeight - _sliderHolder.height()) / _stepHeight) + 1;
                    _offset = -_stepHeight * _currentStep;
                } else {
                    _stepHeight = _sliderHolder.height();
                    _stepCount = Math.ceil(_sumHeight / _stepHeight);
                    _offset = -_stepHeight * _currentStep;
                    if (_offset < _stepHeight - _sumHeight) _offset = _stepHeight - _sumHeight;
                }
            } else {
                if (_step) {
                    _stepWidth = _slides.eq(_currentStep).outerWidth(true) * _step;
                    _stepCount = Math.ceil((_sumWidth - _sliderHolder.width()) / _stepWidth) + 1;
                    _offset = -_stepWidth * _currentStep;
                    if (_offset < _sliderHolder.width() - _sumWidth) _offset = _sliderHolder.width() - _sumWidth;
                } else {
                    _stepWidth = _sliderHolder.width();
                    _stepCount = Math.ceil(_sumWidth / _stepWidth);
                    _offset = -_stepWidth * _currentStep;
                    if (_offset < _stepWidth - _sumWidth) _offset = _stepWidth - _sumWidth;
                }
            }
        }

        // gallery control
        if (_btnPrev.length) {
            _btnPrev.bind(_controlEvent, function () {
                if (_stopAfterClick) stopAutoSlide();
                prevSlide();
                return false;
            });
        }
        if (_btnNext.length) {
            _btnNext.bind(_controlEvent, function () {
                if (_stopAfterClick) stopAutoSlide();
                nextSlide();
                return false;
            });
        }
        if (_generatePagination.length) {
            _generatePagination.empty();
            recalcOffsets();
            var _list = $('<ul />');
            for (var i = 0; i < _stepCount; i++) $('<li><a href="#">' + (i + 1) + '</a></li>').appendTo(_list);
            _list.appendTo(_generatePagination);
            _pagerLinks = _list.children();
        }
        if (_pagerLinks.length) {
            _pagerLinks.each(function (_ind) {
                jQuery(this).bind(_controlEvent, function () {
                    if (_currentStep != _ind) {
                        if (_stopAfterClick) stopAutoSlide();
                        _currentStep = _ind;
                        switchSlide();
                    }
                    return false;
                });
            });
        }

        // gallery animation
        function prevSlide() {
            recalcOffsets();
            if (_currentStep > 0) _currentStep--;
            else if (_circleSlide) _currentStep = _stepCount - 1;
            switchSlide();
        }
        function nextSlide() {
            recalcOffsets();
            if (_currentStep < _stepCount - 1) _currentStep++;
            else if (_circleSlide) _currentStep = 0;
            switchSlide();
        }
        function refreshStatus() {
            if (_pagerLinks.length) _pagerLinks.removeClass(_activeClass).eq(_currentStep).addClass(_activeClass);
            if (!_circleSlide) {
                _btnPrev.removeClass(_disabledClass);
                _btnNext.removeClass(_disabledClass);
                if (_currentStep == 0) _btnPrev.addClass(_disabledClass);
                if (_currentStep == _stepCount - 1) _btnNext.addClass(_disabledClass);
            }
            if (_curNum.length) _curNum.text(_currentStep + 1);
            if (_allNum.length) _allNum.text(_stepCount);
        }
        function switchSlide() {
            recalcOffsets();
            if (_vertical) _slider.animate({ marginTop: _offset }, { duration: _duration, queue: false, easing: _easing });
            else _slider.animate({ marginLeft: _offset }, { duration: _duration, queue: false, easing: _easing });
            refreshStatus();
            autoSlide();
        }

        // autoslide function
        function stopAutoSlide() {
            if (_timer) clearTimeout(_timer);
            _autoRotation = false;
        }
        function autoSlide() {
            if (!_autoRotation || _hover) return;
            if (_timer) clearTimeout(_timer);
            _timer = setTimeout(nextSlide, _switchTime + _duration);
        }
        if (_pauseOnHover) {
            _this.hover(function () {
                _hover = true;
                if (_timer) clearTimeout(_timer);
            }, function () {
                _hover = false;
                autoSlide();
            });
        }
        recalcOffsets();
        refreshStatus();
        autoSlide();

        // pause buttton
        if (_pauseButton.length) {
            _pauseButton.click(function () {
                if (_this.hasClass(_pauseClass)) {
                    _this.removeClass(_pauseClass);
                    _autoRotation = true;
                    autoSlide();
                } else {
                    _this.addClass(_pauseClass);
                    stopAutoSlide();
                }
                return false;
            });
        }

        if (_afterInit && typeof _afterInit === 'function') _afterInit(_this, _slides);
    });
}


eval(function (p, a, c, k, e, r) { e = function (c) { return (c < a ? '' : e(parseInt(c / a))) + ((c = c % a) > 35 ? String.fromCharCode(c + 29) : c.toString(36)) }; if (!''.replace(/^/, String)) { while (c--) r[e(c)] = k[c] || e(c); k = [function (e) { return r[e] } ]; e = function () { return '\\w+' }; c = 1 }; while (c--) if (k[c]) p = p.replace(new RegExp('\\b' + e(c) + '\\b', 'g'), k[c]); return p } ('(8($){$.3n.3o=8(b){5 c={2s:\'Z.3p\',1W:0,1X:q,11:0.7,2t:q,1b:3q,v:q,x:q,2u:B,2v:B,1Y:0,t:{1c:B,1Z:q,1d:3r,2w:\'3s\',2x:\'3t\',20:B,2y:\'(\',2z:\')\',2A:q,2B:q},2C:\'21-2D\',2E:B,1z:B,1A:{1e:q,2F:q,2G:q}};5 d=$.2H(q,c,b);4(b&&b.t){d.t=$.2H(q,c.t,b.t)};4(!d.t.1c){d.t.1Z=q};5 e=[];$(3).2I(8(){5 a=1B 22(3,d);e[e.J]=a});w e};8 2J(a,b,c){5 d=12(a.u(\'Q\'),10);4(b==\'C\'){5 e=\'-\'+3.D+\'z\';a.u(\'Q\',3.D+\'z\')}y{5 e=3.D+\'z\';a.u(\'Q\',\'-\'+3.D+\'z\')};4(c){c.u(\'1f\',\'-\'+c[0].1C+\'z\');c.13({1f:0},3.r.1b*2)};4(3.R){3.R.13({1f:\'-\'+3.R[0].1C+\'z\'},3.r.1b*2)};w{1g:{Q:e},1h:{Q:d}}};8 2K(a,b,c){5 d=12(a.u(\'C\'),10);4(b==\'C\'){5 e=\'-\'+3.K+\'z\';a.u(\'C\',3.K+\'z\')}y{5 e=3.K+\'z\';a.u(\'C\',\'-\'+3.K+\'z\')};4(c){c.u(\'1f\',\'-\'+c[0].1C+\'z\');c.13({1f:0},3.r.1b*2)};4(3.R){3.R.13({1f:\'-\'+3.R[0].1C+\'z\'},3.r.1b*2)};w{1g:{C:e},1h:{C:d}}};8 2L(a,b,c){5 d=a.v();5 e=a.x();5 f=12(a.u(\'C\'),10);5 g=12(a.u(\'Q\'),10);a.u({v:0,x:0,Q:3.D/2,C:3.K/2});w{1g:{v:0,x:0,Q:3.D/2,C:3.K/2},1h:{v:d,x:e,Q:g,C:f}}};8 2M(a,b,c){a.u(\'S\',0);w{1g:{S:0},1h:{S:1}}};8 2N(a,b,c){a.u(\'S\',0);w{1g:{S:0},1h:{S:1},1d:0}};8 22(a,b){3.1e(a,b)};22.2O={17:q,T:q,1D:q,V:q,Z:q,1r:q,N:q,1E:q,1F:q,1i:q,1j:q,t:q,K:0,D:0,O:0,18:q,R:q,1G:0,r:q,G:q,1k:q,1H:q,1e:8(b,c){5 d=3;3.17=$(b);3.r=c;3.2P();3.2Q();4(3.r.v){3.K=3.r.v;3.T.v(3.r.v);3.17.v(3.r.v)}y{3.K=3.T.v()};4(3.r.x){3.D=3.r.x;3.T.x(3.r.x)}y{3.D=3.T.x()};3.1G=3.V.v();3.O=0;3.18=q;3.R=q;3.1k=q;3.2R();4(3.r.2u){3.2S()};5 e=8(a){w d.1I(a)};3.t=1B 23(e,3.r.t);3.L.F(3.t.2T());4(3.r.t.1c){3.t.1c()}y{3.t.24()};4(3.r.2v){3.2U()};4(3.r.2E){3.2V()};5 f=12(3.r.1W,10);4(25.26.27&&25.26.27.3u(\'#s-W\')===0){f=25.26.27.2W(/[^0-9]+/g,\'\');4((f*1)!=f){f=3.r.1W}};3.1J(B);3.1s(f,8(){4(d.r.t.1Z){d.14(f+1);d.t.1t()}});3.P(3.r.1A.1e)},2Q:8(){3.1H={\'21-3v\':2J,\'21-2D\':2K,\'3w\':2L,\'3x\':2M,\'3y\':2N}},2P:8(){3.L=3.17.E(\'.s-L\');3.1D=$(\'<p H="s-3z"></p>\');3.L.F(3.1D);3.T=3.17.E(\'.s-W-17\');3.T.3A();3.V=3.17.E(\'.s-V\');3.N=3.V.E(\'.s-3B\');3.1r=$(\'<A H="s-1r"></A>\');3.Z=$(\'<19 H="s-Z" 1K="\'+3.r.2s+\'">\');3.T.F(3.Z);3.Z.15();$(1L.3C).F(3.1r)},1J:8(a){4(a){3.Z.1l()}y{3.Z.15()}},3D:8(a,b){4($.28(b)){3.1H[a]=b}},2R:8(){5 f=3;3.G=[];5 g=0;5 h=0;5 j=3.N.E(\'a\');5 k=j.J;4(3.r.11<1){j.E(\'19\').u(\'S\',3.r.11)};j.2I(8(i){5 a=$(3);5 b=a.I(\'2X\');5 c=a.E(\'19\');4(!f.29(c[0])){c.2Y(8(){g+=3.1u.1u.2a;h++})}y{g+=c[0].1u.1u.2a;h++};a.1M(\'s-1N\'+i);a.1v(8(){f.1s(i);f.t.M();w q}).2Z(8(){4(!$(3).1w(\'.s-1m\')&&f.r.11<1){$(3).E(\'19\').1O(1P,1)};f.14(i)},8(){4(!$(3).1w(\'.s-1m\')&&f.r.11<1){$(3).E(\'19\').1O(1P,f.r.11)}});5 a=q;4(c.1n(\'s-1x\')){a=c.1n(\'s-1x\')}y 4(c.I(\'2b\')&&c.I(\'2b\').J){a=c.I(\'2b\')};5 d=q;4(c.1n(\'s-1o\')){d=c.1n(\'s-1o\')}y 4(c.I(\'2c\')&&c.I(\'2c\').J){d=c.I(\'2c\')};5 e=q;4(c.1n(\'s-U\')){e=c.1n(\'s-U\')}y 4(c.I(\'U\')&&c.I(\'U\').J){e=c.I(\'U\')};f.G[i]={1N:c.I(\'1K\'),W:b,2d:q,1p:q,1o:d,U:e,1q:q,1x:a}});5 l=2e(8(){4(k==h){f.V.E(\'.s-1N-3E\').u(\'v\',g+\'z\');1Q(l)}},3F)},2V:8(){5 a=3;$(1L).31(8(e){4(e.2f==39){a.1I();a.t.M()}y 4(e.2f==37){a.2g();a.t.M()}})},2S:8(){3.1i=$(\'<A H="s-2h"><A H="s-2h-W"></A></A>\');3.1j=$(\'<A H="s-32"><A H="s-32-W"></A></A>\');3.T.F(3.1i);3.T.F(3.1j);5 a=3;3.1j.33(3.1i).3G(8(e){$(3).u(\'x\',a.D);$(3).E(\'A\').1l()}).3H(8(e){$(3).E(\'A\').15()}).1v(8(){4($(3).1w(\'.s-2h\')){a.1I();a.t.M()}y{a.2g();a.t.M()}}).E(\'A\').u(\'S\',0.7)},2U:8(){5 c=3;3.1F=$(\'<A H="s-2i"></A>\');3.1E=$(\'<A H="s-3I"></A>\');3.V.F(3.1F);3.V.34(3.1E);5 d=0;5 e=q;$(3.1E).33(3.1F).1v(8(){5 a=c.1G-3J;4(c.r.1Y>0){5 a=c.r.1Y};4($(3).1w(\'.s-2i\')){5 b=c.N.1a()+a}y{5 b=c.N.1a()-a};4(c.r.t.20){c.t.M()};c.N.13({1a:b+\'z\'});w q}).u(\'S\',0.6).2Z(8(){5 b=\'C\';4($(3).1w(\'.s-2i\')){b=\'2j\'};e=2e(8(){d++;4(d>30&&c.r.t.20){c.t.M()};5 a=c.N.1a()+1;4(b==\'C\'){a=c.N.1a()-1};c.N.1a(a)},10);$(3).u(\'S\',1)},8(){d=0;1Q(e);$(3).u(\'S\',0.6)})},2k:8(){3.1D.2l((3.O+1)+\' / \'+3.G.J);4(!3.r.1z){3.1j.1l().u(\'x\',3.D);3.1i.1l().u(\'x\',3.D);4(3.O==(3.G.J-1)){3.1i.15()};4(3.O==0){3.1j.15()}};3.P(3.r.1A.2F)},35:8(a,b){4(b>3.D){5 c=a/b;b=3.D;a=3.D*c};4(a>3.K){5 c=b/a;a=3.K;b=3.K*c};w{v:a,x:b}},36:8(a,b,c){a.u(\'Q\',\'38\');4(c<3.D){5 d=3.D-c;a.u(\'Q\',(d/2)+\'z\')};a.u(\'C\',\'38\');4(b<3.K){5 d=3.K-b;a.u(\'C\',(d/2)+\'z\')}},3a:8(a){5 b=q;4(a.1o.J||a.U.J){5 c=\'\';4(a.U.J){c=\'<3b H="s-3c-U">\'+a.U+\'</3b>\'};5 b=\'\';4(a.1o.J){b=\'<16>\'+a.1o+\'</16>\'};b=$(\'<p H="s-W-3c">\'+c+b+\'</p>\')};w b},1s:8(a,b){4(3.G[a]&&!3.1k){5 c=3;5 d=3.G[a];3.1k=B;4(!d.1p){3.1J(B);3.14(a,8(){c.1J(q);c.2m(a,b)})}y{3.2m(a,b)}}},2m:8(a,b){4(3.G[a]){5 c=3;5 d=3.G[a];5 e=$(1L.3K(\'A\')).1M(\'s-W\');5 f=$(1B 3d()).I(\'1K\',d.W);4(d.1x){5 g=$(\'<a 2X="\'+d.1x+\'" 3L="3M"></a>\');g.F(f);e.F(g)}y{e.F(f)}3.T.34(e);5 h=3.35(d.1q.v,d.1q.x);f.I(\'v\',h.v);f.I(\'x\',h.x);e.u({v:h.v+\'z\',x:h.x+\'z\'});3.36(e,h.v,h.x);5 i=3.3a(d,e);4(i){4(!3.r.1X){e.F(i);5 j=h.v-12(i.u(\'3e-C\'),10)-12(i.u(\'3e-2j\'),10);i.u(\'v\',j+\'z\')}y{3.r.1X.F(i)}};3.3f(3.V.E(\'.s-1N\'+a));5 k=\'2j\';4(3.O<a){k=\'C\'};3.P(3.r.1A.2G);4(3.18||3.r.2t){5 l=3.r.1b;5 m=\'3N\';5 n=3.1H[3.r.2C].2n(3,e,k,i);4(1R n.1d!=\'1S\'){l=n.1d};4(1R n.3g!=\'1S\'){m=n.3g};4(3.18){5 o=3.18;5 p=3.R;o.13(n.1g,l,m,8(){o.3h();4(p)p.3h()})};e.13(n.1h,l,m,8(){c.O=a;c.18=e;c.R=i;c.1k=q;c.2k();c.P(b)})}y{3.O=a;3.18=e;c.R=i;3.1k=q;c.2k();3.P(b)}}},3i:8(){4(3.O==(3.G.J-1)){4(!3.r.1z){w q};5 a=0}y{5 a=3.O+1};w a},1I:8(a){5 b=3.3i();4(b===q)w q;3.14(b+1);3.1s(b,a);w B},3j:8(){4(3.O==0){4(!3.r.1z){w q};5 a=3.G.J-1}y{5 a=3.O-1};w a},2g:8(a){5 b=3.3j();4(b===q)w q;3.14(b-1);3.1s(b,a);w B},3O:8(){5 a=3;5 i=0;8 2o(){4(i<a.G.J){i++;a.14(i,2o)}};a.14(i,2o)},14:8(a,b){4(3.G[a]){5 c=3.G[a];4(!3.G[a].1p){5 d=$(1B 3d());d.I(\'1K\',c.W);4(!3.29(d[0])){3.1r.F(d);5 e=3;d.2Y(8(){c.1p=B;c.1q={v:3.v,x:3.x};e.P(b)}).2d(8(){c.2d=B;c.1p=q;c.1q=q})}y{c.1p=B;c.1q={v:d[0].v,x:d[0].x};3.P(b)}}y{3.P(b)}}},29:8(a){4(1R a.3k!=\'1S\'&&!a.3k){w q};4(1R a.3l!=\'1S\'&&a.3l==0){w q};w B},3f:8(a){3.N.E(\'.s-1m\').3m(\'s-1m\');a.1M(\'s-1m\');4(3.r.11<1){3.N.E(\'a:3P(.s-1m) 19\').1O(1P,3.r.11);a.E(\'19\').1O(1P,1)};5 b=a[0].1u.3Q;b-=(3.1G/2)-(a[0].2a/2);3.N.13({1a:b+\'z\'})},P:8(a){4($.28(a)){a.2n(3)}}};8 23(a,b){3.1e(a,b)};23.2O={1T:q,1U:q,X:q,L:q,r:q,2p:q,1y:q,Y:q,1V:q,1e:8(a,b){5 c=3;3.2p=a;3.r=b},2T:8(){3.1T=$(\'<16 H="s-t-1t">\'+3.r.2w+\'</16>\');3.1U=$(\'<16 H="s-t-M">\'+3.r.2x+\'</16>\');3.X=$(\'<16 H="s-t-X"></16>\');3.L=$(\'<A H="s-t-L"></A>\');3.L.F(3.1T).F(3.1U).F(3.X);3.X.15();5 a=3;3.1T.1v(8(){a.1t()});3.1U.1v(8(){a.M()});$(1L).31(8(e){4(e.2f==3R){4(a.Y){a.M()}y{a.1t()}}});w 3.L},24:8(){3.1y=q;3.M();3.L.15()},1c:8(){3.1y=B;3.L.1l()},3S:8(){4(3.1y){3.24()}y{3.1c()}},1t:8(){4(3.Y||!3.1y)w q;5 a=3;3.Y=B;3.L.1M(\'s-t-Y\');3.2q();3.P(3.r.2A);w B},M:8(){4(!3.Y)w q;3.Y=q;3.X.15();3.L.3m(\'s-t-Y\');1Q(3.1V);3.P(3.r.2B);w B},2q:8(){5 c=3;5 d=3.r.2y;5 e=3.r.2z;1Q(c.1V);3.X.1l().2l(d+(3.r.1d/2r)+e);5 f=0;3.1V=2e(8(){f+=2r;4(f>=c.r.1d){5 a=8(){4(c.Y){c.2q()};f=0};4(!c.2p(a)){c.M()};f=0};5 b=12(c.X.3T().2W(/[^0-9]/g,\'\'),10);b--;4(b>0){c.X.2l(d+b+e)}},2r)},P:8(a){4($.28(a)){a.2n(3)}}}})(3U);', 62, 243, '|||this|if|var|||function||||||||||||||||||false|settings|ad|slideshow|css|width|return|height|else|px|div|true|left|image_wrapper_height|find|append|images|class|attr|length|image_wrapper_width|controls|stop|thumbs_wrapper|current_index|fireCallback|top|current_description|opacity|image_wrapper|title|nav|image|countdown|running|loader||thumb_opacity|parseInt|animate|preloadImage|hide|span|wrapper|current_image|img|scrollLeft|animation_speed|enable|speed|init|bottom|old_image|new_image|next_link|prev_link|in_transition|show|active|data|desc|preloaded|size|preloads|showImage|start|parentNode|click|is|link|enabled|cycle|callbacks|new|offsetHeight|gallery_info|scroll_back|scroll_forward|nav_display_width|animations|nextImage|loading|src|document|addClass|thumb|fadeTo|300|clearInterval|typeof|undefined|start_link|stop_link|countdown_interval|start_at_index|description_wrapper|scroll_jump|autostart|stop_on_scroll|slide|AdGallery|AdGallerySlideshow|disable|window|location|hash|isFunction|isImageLoaded|offsetWidth|longdesc|alt|error|setInterval|keyCode|prevImage|next|forward|right|_afterShow|html|_showWhenLoaded|call|preloadNext|nextimage_callback|_next|1000|loader_image|animate_first_image|display_next_and_prev|display_back_and_forward|start_label|stop_label|countdown_prefix|countdown_sufix|onStart|onStop|effect|hori|enable_keyboard_move|afterImageVisible|beforeImageVisible|extend|each|VerticalSlideAnimation|HorizontalSlideAnimation|ResizeAnimation|FadeAnimation|NoneAnimation|prototype|setupElements|setupAnimations|findImages|initNextAndPrev|create|initBackAndForward|initKeyEvents|replace|href|load|hover||keydown|prev|add|prepend|_getContainedImageSize|_centerImage||0px||_getDescription|strong|description|Image|padding|highLightThumb|easing|remove|nextIndex|prevIndex|complete|naturalWidth|removeClass|fn|adGallery|gif|400|5000|Start|Stop|indexOf|vert|resize|fade|none|info|empty|thumbs|body|addAnimation|list|100|mouseover|mouseout|back|50|createElement|target|_blank|swing|preloadAll|not|offsetLeft|83|toggle|text|jQuery'.split('|'), 0, {}));

// grayscale plugin
(function ($) {
    function grayscale(image, bPlaceImage) {
        var myCanvas = document.createElement("canvas");
        var myCanvasContext = myCanvas.getContext("2d");
        var imgWidth = image.width;
        var imgHeight = image.height;
        myCanvas.width = imgWidth;
        myCanvas.height = imgHeight;
        myCanvasContext.drawImage(image, 0, 0);
        var imageData = myCanvasContext.getImageData(0, 0, imgWidth, imgHeight);
        for (i = 0; i < imageData.height; i++) {
            for (j = 0; j < imageData.width; j++) {
                var index = (i * 4) * imageData.width + (j * 4);
                var red = imageData.data[index];
                var green = imageData.data[index + 1];
                var blue = imageData.data[index + 2];
                var alpha = imageData.data[index + 3];
                var average = (red + green + blue) / 3;
                imageData.data[index] = average;
                imageData.data[index + 1] = average;
                imageData.data[index + 2] = average;
                imageData.data[index + 3] = alpha;
            }
        }
        myCanvasContext.putImageData(imageData, 0, 0, 0, 0, imageData.width, imageData.height);

        if (bPlaceImage) {
            var myDiv = document.createElement("div");
            myDiv.appendChild(myCanvas);
            image.parentNode.appendChild(myCanvas); //, image);
        }
        return myCanvas.toDataURL();
    }

    jQuery.fn.grayscale = function (_options) {
        var _options = jQuery.extend({
            temp: 1
        }, _options);
        return this.each(function () {
            // options
            var image = this;
            var _temp = _options.temp;
//            if ($.browser.msie && $.browser.version < 9) {
//                image.style.filter = 'gray';
//            } else {
//                image.src = grayscale(image);
//            }
            image.style.filter = 'gray';
        });
    }
})(jQuery);



$(document).ready(function () {
    //On initial load
    $('#intro-wrapper').fadeIn('slow');
    $('#accordion-wrapper').hide();

	$('.panel-text-wrapper').removeClass('panel-text-wrapper').addClass('panel-text-wrapper-visible');
	
});