module.exports = {
    darkMode: 'class',
    future: {
        removeDeprecatedGapUtilities: true,
        purgeLayersByDefault: false,
    },
    content: [
        "./Pages/**/*.{razor,html,cshtml}",
        "./Shared/**/*.{razor,html,cshtml}",
        "./wwwroot/index.html"
    ],
    theme: {
        extend: {
            backgroundImage: {
                // Premium patterns
                'premium-pattern-light': "linear-gradient(rgba(255, 255, 255, 0.5), rgba(255, 255, 255, 0.5)), url('https://cdn.colorchan.com/site/gradiant_bg_8bit_comp.png')",
                'premium-pattern': "url('https://cdn.colorchan.com/site/gradiant_bg_8bit_comp.png')",
                // Color circles
                'color-circle': "url('https://cdn.colorchan.com/site/color-circle.webp')",
                'color-circle-opaque': "linear-gradient(rgba(255, 255, 255, 0.7), rgba(255, 255, 255, 0.7)), url('https://cdn.colorchan.com/site/color-circle.webp')",
                // Footer
                'footer': "url('https://cdn.colorchan.com/site/footer-bg.png')",
                // Index header
                'header': "url('https://cdn.colorchan.com/site/index-header-circle-bg-comp.webp')"
            },
            backgroundSize: {
                '100%': '100%',
                '75%': '75%',
                '50%': '50%',
                '25%': '25%'
            },
            colors: {
                'ultra-light-gray': '#fcfcfc',
                'discord': '#7289DA'
            },
            height: {
                '88': '22rem',
                '104': '26rem',
                '112': '28rem',
                '120': '30rem',
                '128': '32rem',
                '136': '34rem',
            },
            width: {
                '88': '22rem',
                '104': '26rem',
                '112': '28rem',
                '120': '30rem',
                '128': '32rem',
                '136': '34rem',
            },
            spacing: {
                '22': '5.5rem',
                '26': '6.5rem'
            }
        },
        fontFamily: {
            'helvetica': ['Helvetica Neue', 'sans-serif'],
            'helvetica-black': ['Helvetica Neue black', 'sans-serif']
        }
    },
    variants: {},
    plugins: [],
}
