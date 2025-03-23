updateDarkModeSetting();

// noinspection JSUnusedGlobalSymbols
function useLightMode() {
    localStorage.theme = 'light';
    updateDarkModeSetting();
}

// noinspection JSUnusedGlobalSymbols
function useDarkMode() {
    localStorage.theme = 'dark';
    updateDarkModeSetting();
}

// noinspection JSUnusedGlobalSymbols
function respectOsPreference() {
    localStorage.removeItem('theme');
    updateDarkModeSetting();
}

// noinspection JSUnusedGlobalSymbols
function updateDarkModeSetting() {
    // On page load or when changing themes, best to add inline in `head` to avoid FOUC
    if (localStorage.theme === 'dark' || (!('theme' in localStorage) && window.matchMedia('(prefers-color-scheme: dark)').matches)) {
        document.documentElement.classList.add('dark')
        console.log("Added dark mode");
    } else {
        document.documentElement.classList.remove('dark')
        console.log("Removed dark mode");
    }
}

// noinspection JSUnusedGlobalSymbols
function isDarkMode() {
    return localStorage.theme === 'dark' || (!('theme' in localStorage) && window.matchMedia('(prefers-color-scheme: dark)').matches);
}

// noinspection JSUnusedGlobalSymbols
function initMasonry(containerSelector, gutterSize, items) {
    let magicGrid = new MagicGrid({
        container: containerSelector,
        animate: true,
        gutter: gutterSize,
        items: items,
        useMin: true,
        center: false
    });

    magicGrid.listen();
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