$(document).ready(function () {
  // --------------------------------------------------------------------------------------
	// GENERAL ------------------------------------------------------------------------------
	var $window = $(window);

	$window.on('scroll', function() {
    if ($window.scrollTop() > 50) {
      $("body").addClass("scrolled");
    } else {
      $("body").removeClass("scrolled");
    }
	}).trigger("scroll");
	
	// MENU
  $(".menu-open").on('click', function() {
    $("html").toggleClass("menu-open");
    return false;
  });

  $("#menu .menu-close, #menu-blocker").on('click', function() {
    $("html").removeClass("menu-open");
    return false;
  });
  
  if ($("body").is("#hom")) {

      // TOLDO
      $(".toldo").prepend("<div class='guir guir-l'></div><div class='guir guir-r'></div>");

    function onSlide1() {
      $(".slide1 p, .slide1 span, .slide1 div").css({"opacity": 0});
      $(".slide1 p").animate({"opacity": 1}, 150);
      $(".slide1 span").delay(250).animate({"opacity": 1}, 150);
      $(".slide1 div").delay(450).animate({"opacity": 1}, 150);
    }

    function onSlide2() {
      $(".slide2 p, .slide2 img, .slide2 div").css({"opacity": 0});
      $(".slide2 div").delay(450).animate({"opacity": 1}, 150);
      $(".slide2 p").animate({"opacity": 1}, 150);
      $(".slide2 img").each(function(index) {
			  $(".slide2 .img"+(index+1)+"").delay(150*index).animate({"opacity": 1}, 250);
		  });
    }

		$(".anim .owl-carousel").owlCarousel({
			loop: true,
      margin: 0,
      mouseDrag: true,
      touchDrag: false,
      autoplay: false,
      animateOut: 'fadeOut',
			autoplayTimeout: 5000,
			autoplaySpeed: 0,
			autoplayHoverPause: false,
			items: 1,
		}).on('changed.owl.carousel', function(event) {
      switch (event.item.index) {
        case 3:
          onSlide2();
          break;
        case 4:
          onSlide1();
          break;
      }
    });

    onSlide1();

		$("main .owl-carousel").owlCarousel({
			loop: true,
			margin: 0,
			autoplay: true,
			slideTransition: "linear",
			autoplayTimeout: 0,
			autoplaySpeed: 3000,
			autoplayHoverPause: false,
			responsive: {
				0: { items: 3 },
				450: { items: 4 },
				600: { items: 5 },
				750: { items: 6 },
				900: { items: 8 },
			},
    });
    
	} else {

    if($("main").hasClass("conElems")) {
      $("main").prepend("<div class='elems'></div>");
      var nums = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12];
      shuffle(nums);
      $.each(nums, function(i) {
        $(".elems").append("<img src='/images/elem-"+(i+1)+".png' class='pos"+nums[i]+"' />");
      })
    }

    $(".nano").nanoScroller({ alwaysVisible: true });
  }

});


function shuffle(array) {
  var currentIndex = array.length, temporaryValue, randomIndex;

  // While there remain elements to shuffle...
  while (0 !== currentIndex) {

    // Pick a remaining element...
    randomIndex = Math.floor(Math.random() * currentIndex);
    currentIndex -= 1;

    // And swap it with the current element.
    temporaryValue = array[currentIndex];
    array[currentIndex] = array[randomIndex];
    array[randomIndex] = temporaryValue;
  }

  return array;
}