updateDarkModeSetting();

function useLightMode() {
    localStorage.theme = 'light';
    updateDarkModeSetting();
}

function useDarkMode() {
    localStorage.theme = 'dark';
    updateDarkModeSetting();
}

function respectOsPreference() {
    localStorage.removeItem('theme');
    updateDarkModeSetting();
}

function updateDarkModeSetting() {
    // On page load or when changing themes, best to add inline in `head` to avoid FOUC
    if (localStorage.theme === 'dark' || (!('theme' in localStorage) && window.matchMedia('(prefers-color-scheme: dark)').matches)) {
        document.documentElement.classList.add('dark')
    } else {
        document.documentElement.classList.remove('dark')
    }
}

function isDarkMode() {
    return localStorage.theme === 'dark';
}

// noinspection JSUnusedGlobalSymbols
function loadJs(sourceUrl, shouldDefer) {
    if (sourceUrl.Length === 0) {
        console.error("Invalid source URL");
        return;
    }

    let tag = document.createElement('script');
    tag.async = shouldDefer;
    tag.src = sourceUrl;
    tag.type = "text/javascript";

    tag.onload = function () {
        console.log("Script loaded successfully");
    }

    tag.onerror = function () {
        console.error("Failed to load script");
    }

    document.head.appendChild(tag);
}