//swipper
var swiper = new Swiper(".trending-swiper-slide", {
    slidesPerView: 2,
    spaceBetween: 10,

    autoplay: {
        delay: 2300,
        disableOnInteraction: false,
    },
    pagination: {
        el: ".swiper-pagination",
        clickable: true,
    },
    navigation: {
        nextEl: ".swiper-button-next",
        prevEl: ".swiper-button-prev",
    },
    breakpoints: {
        200: {
            slidesPerView: 1,
            spaceBetween: 30,

        },
        320: {
            slidesPerView: 2,
            spaceBetween: 30,

        },
        510: {
            slidesPerView: 2,
            spaceBetween: 30,

        },
        758: {
            slidesPerView: 3,
            spaceBetween: 35,

        },
        1200: {
            slidesPerView: 4,
            spaceBetween: 30,

        },
    },
});
// //show video
// let playButton = document.querySelector(".play-movie");
// let video = document.querySelector(".video-container");
// let myvideo = document.querySelector(".#myvideo");

// let closebtn = document.querySelector(".close-video");

// playButton.onclick = () => {
//     video.classList.add(".show-video");
// };
var swiper = new Swiper(".mySwiper", {
    spaceBetween: 30,
    centeredSlides: true,
    autoplay: {
        delay: 2500,
        disableOnInteraction: false,
    },
    pagination: {
        el: ".swiper-pagination",
        clickable: true,
    },
    navigation: {
        nextEl: ".swiper-button-next",
        prevEl: ".swiper-button-prev",
    },
});