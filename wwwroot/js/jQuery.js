$(document).ready(function() {

  // --------------------------------------------------------------------------------------
  // GENERAL ------------------------------------------------------------------------------
  var $window = $(window);

  // CIERRE
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
  // MENU NAVEGACION ----------------------------------------------------------------------
  $(".menu-open").click(function() {
    $("body").toggleClass("menu-open");
    return false;
  });

  $("#menu .menu-close, #menu-blocker").click(function() {
    $("body").removeClass("menu-open");
    return false;
  });

  // --------------------------------------------------------------------------------------
  // ANIMACION HOME -----------------------------------------------------------------------
  // "HOME" -------------------------------------------------
  if ($("body").is("#hom")) {
    $(".anim .owl-carousel").owlCarousel({
      touchDrag: false,
      mouseDrag: false,
      autoplay: true,
      autoplayHoverPause: false,
      autoplayTimeout: 5000,
      autoplaySpeed: 1000,
      items: 1,
      // animateOut: 'fadeOut',
      animateOut: 'flipOutY fadeOut',
      animateIn: 'flipInY fadeIn',
      dots: false,
      nav: false,
      loop: true
    });
  
    $(".productos a").fancybox({
      toolbar: false,
      smallBtn: true,
      iframe: {
        css: { width : '400px'}
      }
    });
  
    $(".logos").owlCarousel({
      touchDrag: true,
      autoplay: false,
      autoplayHoverPause: false,
      autoplayTimeout: 2000,
      autoplaySpeed: 1000,
      navSpeed: 500,
      navText: ["",""],
      items: 1,
      dots: false,
      loop: true,
      nav: true,
      responsive: {
        0: { margin: 30, stagePadding: 20 },
        360: { margin: 50, stagePadding: 40 },
        480: { margin: 70, stagePadding: 60 },
        768: { margin: 100, stagePadding: 90 }
      }
    });
  } else if ($("body").is("#int")) {
    $(".nano:not(.noscroll)").nanoScroller({ alwaysVisible: true });
  }

});