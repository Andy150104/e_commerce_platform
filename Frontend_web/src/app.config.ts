export default defineAppConfig({
  ui: {
    button: {
      color: {
        blue: {
          solid:
            'shadow-sm text-white bg-blue-500 hover:bg-blue-600 disabled:bg-blue-500 aria-disabled:bg-blue-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-blue-500',
          ghost: 'text-blue-500 hover:bg-blue-50 ...',
        },
      },
    },
    notifications: {
      position: 'top-0 right-0',
    },
  },
})
