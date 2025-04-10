// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Mobile Menu Toggle
const mobileMenuButton = document.getElementById('mobile-menu-button');
const mobileMenu = document.getElementById('mobile-menu');

mobileMenuButton.addEventListener('click', () => {
    const isHidden = mobileMenu.classList.contains('hidden');
    if (isHidden) {
        mobileMenu.classList.remove('hidden');
        mobileMenu.classList.add('block');
    } else {
        mobileMenu.classList.add('hidden');
        mobileMenu.classList.remove('block');
    }
});

// Close menu when clicking outside
document.addEventListener('click', (event) => {
    if (!mobileMenu.contains(event.target) && !mobileMenuButton.contains(event.target)) {
        mobileMenu.classList.add('hidden');
        mobileMenu.classList.remove('block');
    }
});

function handleSubscribe(e) {
    e.preventDefault();
    const email = document.getElementById('subscribeEmail').value;

    // Basic email validation
    if (/^\S+@\S+\.\S+$/.test(email)) {
        fetch('/api/Subscribe', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ email: email })
        }).then(response => {
            if (response.ok) {
                showSubscribePopup();
                document.getElementById('subscribeEmail').value = '';
            } else {
                alert('Something went wrong. Please try again.');
            }
        })
        .catch(error => {
            console.error('Error:', error);
            alert('An error occurred. Please try again later.');
        });
    }
}

function showSubscribePopup() {
    const popup = document.getElementById('subscribePopup');
    popup.classList.remove('hidden');
    popup.setAttribute('aria-hidden', 'false');
}

function closeSubscribePopup() {
    const popup = document.getElementById('subscribePopup');
    popup.classList.add('hidden');
    popup.setAttribute('aria-hidden', 'true');
}

// Close popup when clicking outside
document.getElementById('subscribePopup').addEventListener('click', (e) => {
    if (e.target === document.getElementById('subscribePopup')) {
        closeSubscribePopup();
    }
});
