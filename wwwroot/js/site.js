/**
 * DatShin Movie Portal - Swiper Sliders Initialization
 */
document.addEventListener("DOMContentLoaded", function () {
    //  Hero Banner Swiper Slider 
    const heroSwiperEl = document.querySelector(".mySwiper");
    if (heroSwiperEl) {
        new Swiper(".mySwiper", {
            slidesPerView: 1,
            spaceBetween: 0,
            loop: true,
            observer: true,
            observeParents: true,
            watchSlidesProgress: true,
            navigation: {
                nextEl: "#hero-next",
                prevEl: "#hero-prev",
            },
        });
    }

    // ၂။ Trending Movies Grid Swiper Slider
    const moviesContentEl = document.querySelector(".movies-content");
    if (moviesContentEl) {
        new Swiper(".movies-content", {
            slidesPerView: 4,     
            spaceBetween: 20,      
            loop: false,
            grabCursor: true,
            observer: true,
            observeParents: true,
            navigation: {
                nextEl: "#movies-next",
                prevEl: "#movies-prev",
            },
            breakpoints: {
                280: {
                    slidesPerView: 2,
                    spaceBetween: 10
                },
                576: {
                    slidesPerView: 2,
                    spaceBetween: 10
                },
                768: {
                    slidesPerView: 3,
                    spaceBetween: 15
                },
                1024: {
                    slidesPerView: 4,
                    spaceBetween: 20
                }
            }
        });
    }
});