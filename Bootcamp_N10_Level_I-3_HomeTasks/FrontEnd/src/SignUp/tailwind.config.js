export default {
  content: [
    './SignUp.html',
    './src/**/*.{vue,js,ts,jsx,tsx}'
  ],
  theme: {
    screens:{
      sm: '545px',
      md: '960px',
      lg: '1280px'
    },
    width:{
      '440':'440px'
    },
    height:{
      '400': '400px'
    },
    extend: {
      colors: {
        textPrimary: '#222222',
        textSecondary: '#818181',
        logoSelected: '#000000',
        logoSecondary: '#717171',
        logoAccent: '#dddddd',
        logoPrimary: '#ff385c',
        borderSecondary: '#cccccc',
        borderCol: '#162938'
      },
    },
  },
  plugins: [],
}