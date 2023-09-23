const plugin = require('tailwindcss/plugin');

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
            backgroundImage: {},
            backgroundSize: {
                '100%': '100%',
                '75%': '75%',
                '50%': '50%',
                '25%': '25%'
            },
            colors: {
                'ultra-light-gray': '#fcfcfc',
                'discord': '#7289DA',

                // Main color pallet
                'primary': withOpacityValue('#009c82'),
                'primary-dark': withOpacityValue('#53ceb1'),
                'primary-variant': withOpacityValue('#85cee4'),
                'primary-variant-dark': withOpacityValue('#b8ffff'),
                'secondary': withOpacityValue('#c6c5bf'),
                'secondary-variant': withOpacityValue('#c6c5bf'),
                'secondary-dark': withOpacityValue('#f9f8f2'),
                'background': withOpacityValue('#F4F4F4'),
                'background-dark': withOpacityValue('#121212'),
                'surface': withOpacityValue('#FFFFFF'),
                'surface-dark': withOpacityValue('#333333'),
                'error': withOpacityValue('#B00020'),
                'error-dark': withOpacityValue('#CF6679'),
                'on-primary': withOpacityValue('#FFFFFF'),
                'on-primary-dark': withOpacityValue('#000000'),
                'on-primary-variant': withOpacityValue('#000000'),
                'on-primary-variant-dark': withOpacityValue('#000000'),
                'on-secondary': withOpacityValue('#000000'),
                'on-secondary-dark': withOpacityValue('#000000'),
                'on-background': withOpacityValue('#000000'),
                'on-background-dark': withOpacityValue('#FFFFFF'),
                'on-surface': withOpacityValue('#000000'),
                'on-surface-dark': withOpacityValue('#FFFFFF'),
                'on-error': withOpacityValue('#FFFFFF'),
                'on-error-dark': withOpacityValue('#000000')
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
    plugins: [
        plugin(function ({ addUtilities }) {
            addUtilities({
                '.drag-none': {
                    '-webkit-user-drag': 'none',
                    '-khtml-user-drag': 'none',
                    '-moz-user-drag': 'none',
                    '-o-user-drag': 'none',
                    'user-drag': 'none'
                }
            });
        })
    ]
}

function withOpacityValue(hex) {
    const red = parseInt(hex[1] + hex[2], 16);
    const green = parseInt(hex[3] + hex[4], 16);
    const blue = parseInt(hex[5] + hex[6], 16);

    return ({opacityValue}) => {
        if (opacityValue === undefined) {
            return `rgb(${red} ${green} ${blue})`
        }
        return `rgb(${red} ${green} ${blue} / ${opacityValue})`
    }
}