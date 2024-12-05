// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]')
const tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl))

function removeParameters() {
    // Get the current URL without query parameters
    const url = window.location.origin + window.location.pathname;

    // Redirect to the new URL
    window.location.href = url;
}

$(document).ready(function () {
    let isResizing = false;
    let isVertical = true;
    let activeSlider = null;

    $('.split-slider').on('mousedown', function (e) {
        isResizing = true;

        activeSlider = $(this);
        activeSlider.parent().css('user-select', 'none');
        isVertical = activeSlider.hasClass('split-vertical');

        $('body').css('cursor', isVertical ? 'ew-resize' : 'ns-resize'); // Change cursor to resizing mode
    });

    $(document).on('mousemove', function (e) {
        if (!isResizing) return;

        if (isVertical) {
            const containerOffset = activeSlider.parent().offset().left;
            const containerWidth = activeSlider.parent().outerWidth();
            const sliderWidth = activeSlider.outerWidth();

            const newLeftWidth = (e.pageX - (sliderWidth / 2)) - containerOffset;
            const newRigthWidth = containerWidth - newLeftWidth - sliderWidth;

            if (newLeftWidth < sliderWidth * 3) {
                newLeftWidth = sliderWidth * 3;
            }
            if (newRigthWidth < sliderWidth * 3) {
                newRigthWidth = sliderWidth * 3;
            }

            // Update left and right panel sizes
            $('.split-left').css('flex', `0 0 ${newLeftWidth}px`);
            $('.split-right').css('flex', `0 0 ${newRigthWidth}px`);
        }
        else {
            const containerOffset = activeSlider.parent().offset().top;
            const containerHeight = activeSlider.parent().outerHeight();
            const sliderHeight = activeSlider.outerHeight();

            const newTopHeight = (e.pageY - (sliderHeight / 2)) - containerOffset;
            const newBottomHeight = containerHeight - newTopHeight - sliderHeight;

            if (newTopHeight < sliderHeight * 3) {
                newTopHeight = sliderHeight * 3;
            }
            if (newBottomHeight < sliderHeight * 3) {
                newBottomHeight = sliderHeight * 3;
            }

            // Update top and bottom panel sizes
            $('.split-top').css('flex', `0 0 ${newTopHeight}px`);
            $('.split-bottom').css('flex', `0 0 ${newBottomHeight}px`);
        }
    });

    $(document).on('mouseup', function () {
        if (isResizing) {
            isResizing = false;
            $('.slider-container').css('user-select', 'auto');
            $('body').css('cursor', 'auto'); // Reset cursor to default
        }
    });
});