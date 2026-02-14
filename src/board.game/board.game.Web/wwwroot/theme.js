// Theme: persist and apply light/dark. Run immediately so no flash.
(function () {
    var key = 'boardgame-theme';
    var stored = localStorage.getItem(key) || 'light';
    document.documentElement.setAttribute('data-theme', stored);

    window.boardgameTheme = {
        get: function () { return document.documentElement.getAttribute('data-theme') || 'light'; },
        set: function (theme) {
            if (theme !== 'light' && theme !== 'dark') theme = 'light';
            localStorage.setItem(key, theme);
            document.documentElement.setAttribute('data-theme', theme);
            return theme;
        }
    };
})();
