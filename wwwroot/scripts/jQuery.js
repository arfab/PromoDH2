$(document).on('ready', function() {

  // --------------------------------------------------------------------------------------
  // GENERAL ------------------------------------------------------------------------------
  var $window = $(window);
  var activeCarousel;

  // const animated = $('.animated');

  // $(window).on('scroll', function() {
	// 	if (animated) {
	// 		animated.each(function(_, elem) {
	// 			if (isElementInViewport(elem)) {
	// 				elem.classList.add('is-visible');
	// 			} else {
	// 				elem.classList.remove('is-visible');
	// 			}
	// 		});
	// 	}
  // }).trigger('scroll');

  // var $window = $(window);
  // var animRatio;

  // $.fancybox.open({
  //   src: 'cierre.html',
  //   type: 'iframe',
  //   toolbar: false,
  //   smallBtn: true,
  //   opts: {
  //     iframe: {
  //       preload: false,
  //       css: {
  //         width: '600px',
  //       }
  //     }
  //   }
  // });

  // --------------------------------------------------------------------------------------
  // ANIMACION HOME -----------------------------------------------------------------------
  setMobile();

  $window.on('resize', function() {
    if ($window.width() <= 600) {
      if($("body").hasClass("d")) {
        setMobile();
      }
    } else {
      if($("body").hasClass("m")) {
        setDesktop();
      }
    }

  }).trigger("resize");

  function setMobile() {
    $("body").removeClass("d").addClass("m");
    activeCarousel = $("#hom .anim-m .owl-carousel").owlCarousel({
      touchDrag: false,
      mouseDrag: false,
      autoplay: true,
      autoplayHoverPause: false,
      autoplayTimeout: 2000,
      autoplaySpeed: 250,
      items: 1,
      animateOut: 'fadeOut',
      dots: false,
      nav: false,
      loop: true,
      // startPosition: 1
    });
  }

  function setDesktop() {
    $("body").removeClass("m").addClass("d");
    if ($("#hom .anim-m .owl-loaded").length) {
      activeCarousel.trigger('destroy.owl.carousel').trigger('refresh.owl.carousel');
    }
  }


  // if ($("body").hasClass("premio")) {
  //   var newElems = "";
  //   if ($(".premios").hasClass("premio0")) {
  //     newElems += "<img src='images/elem-26.gif' class='elem-premio0-1'>";
  //     newElems += "<img src='images/elem-9.gif' class='elem-premio0-2'>";
  //   } else if ($(".premios").hasClass("premio1")) {
  //     newElems += "<img src='images/elem-1.gif' class='elem-premio1-1'>";
  //     newElems += "<img src='images/elem-4.gif' class='elem-premio1-2'>";
  //     newElems += "<img src='images/elem-18.gif' class='elem-premio1-3'>";
  //   } else if ($(".premios").hasClass("premio2")) {
  //     newElems += "<img src='images/elem-27.gif' class='elem-premio2-1'>";
  //     newElems += "<img src='images/elem-24.gif' class='elem-premio2-2'>";
  //   } else if ($(".premios").hasClass("premio3")) {
  //     newElems += "<img src='images/elem-28.gif' class='elem-premio3-1'>";
  //     newElems += "<img src='images/elem-6.gif' class='elem-premio3-2'>";
  //   }
  //   $(".elems").append(newElems);
  // }




  $(".nano:not(.noscroll)").nanoScroller({ alwaysVisible: true });

  $("#hom .productos a").fancybox({
    toolbar: false,
    smallBtn: true,
    iframe: {
      css: { width : '400px'}
    }
  });

  $("#hom .logos").owlCarousel({
    touchDrag: true,
    autoplay: true,
    autoplayHoverPause: false,
    autoplayTimeout: 2000,
    autoplaySpeed: 1000,
    navSpeed: 500,
    navText: ["",""],
    items: 1,
    //animateOut: 'fadeOut',
    dots: false,
    loop: true,
    nav: true,
    responsive: {
      0: { items: 2, margin: 30, stagePadding: 30 },
      // 360: { margin: 20, stagePadding: 20 },
      // 600: { items: 2, margin: 30, stagePadding: 30 },
      768: { items: 3, margin: 40, stagePadding: 40 }
    }
  });

	const linkmira = $('#hom .productos .mira');
	
	setInterval(function() {
		linkmira.addClass('blink');
		setTimeout(function() { linkmira.removeClass('blink') }, 1000);
	}, 3000);

  // --------------------------------------------------------------------------------------
  // MENU NAVEGACION ----------------------------------------------------------------------
  $(".menu-open").click(function() {
    $("body").toggleClass("menu-open");
    return false;
  });

  $("#menu .menu-close, #menu-blocker").click(function() {
    $("body").removeClass("menu-open");
    return false;
  });

});