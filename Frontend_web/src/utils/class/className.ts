interface ClassNames {
  DARK_THEME: string
  LIGHT_THEME: string
  SYSTEM_THEME: string
  INPUT: string
  INPUT_ERROR: string
  INPUT_LABEL_FLOAT: string
  LABEL_FLOAT: string
  LABEL_DEFAULT: string
  INPUT_LABEL_FLOAT_ERROR: string
  LABEL_FLOAT_ERROR: string
  LABEL_DEFAULT_ERROR: string
  COLS_1: string
  COLS_2: string
  COLS_3: string
  COLS_4: string
  DATE_TIME_PICKER_TODAY: string
  BUTTON_GRADIENT_1: String
  BUTTON_GRADIENT_2: String
  BUTTON_DEFAULT_WHITE: String
  BUTTON_DEFAULT_BLUE_1: String
  BUTTON_DEFAULT_BLUE_2: String
  BUTTON_GRADIENT_BLUE_1: String
  BUTTON_DEFAULT_GRAY_1: String
  BUTTON_RED_1: string
  SELECT_INPUT: String
}

export const className: ClassNames = {
  DARK_THEME: 'dark',
  LIGHT_THEME: 'light',
  SYSTEM_THEME: 'system',
  INPUT:
    'w-full bg-white placeholder:text-slate-600 text-slate-700 text-sm border border-slate-200 rounded-md px-3 py-3 mt-3 mb-3 transition duration-300 ease focus:outline-none focus:border-blue-500 hover:border-blue-300 shadow-sm focus:shadow disabled:bg-slate-100 disabled:text-slate-400 disabled:border-slate-200 dark:bg-gray-900 dark:text-white',
  INPUT_ERROR:
    'w-full bg-transparent placeholder:text-red-400 text-red-700 text-sm border border-red-500 rounded-md px-3 py-3 mt-3 mb-3 transition duration-300 ease focus:outline-none focus:border-red-500 hover:border-red-500 shadow-sm focus:shadow',
  INPUT_LABEL_FLOAT:
    'block px-2.5 pb-2.5 pt-4 w-full text-sm text-gray-900 bg-transparent rounded-lg border-1 border-gray-300 appearance-none dark:text-white dark:border-gray-600 dark:focus:border-blue-500 focus:outline-none focus:ring-0 focus:border-blue-600 peer',
  LABEL_FLOAT:
    'absolute text-sm text-gray-500 dark:text-gray-400 duration-300 transform -translate-y-4 scale-75 top-2 z-10 origin-[0] bg-white dark:bg-gray-900 px-2 peer-focus:px-2 peer-focus:text-blue-600 peer-focus:dark:text-blue-500 peer-placeholder-shown:scale-100 peer-placeholder-shown:-translate-y-1/2 peer-placeholder-shown:top-1/2 peer-focus:top-2 peer-focus:scale-75 peer-focus:-translate-y-4 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto start-1',
  LABEL_DEFAULT: 'block mb-2 mt-0 md:mt-3 text-sm font-medium text-gray-900 dark:text-white',
  INPUT_LABEL_FLOAT_ERROR:
    'block px-2.5 pb-2.5 pt-4 w-full text-sm text-red-500 bg-transparent rounded-lg border-1 border-red-500 appearance-none dark:text-red-500 dark:border-red-500 dark:focus:border-red-500 focus:outline-none focus:ring-0 focus:border-red-500 peer',
  LABEL_FLOAT_ERROR:
    'absolute text-sm text-red-500 dark:text-red-500 duration-300 transform -translate-y-4 scale-75 top-2 z-10 origin-[0] bg-white dark:bg-red-500 px-2 peer-focus:px-2 peer-focus:text-red-500 peer-focus:dark:text-red-500 peer-placeholder-shown:scale-100 peer-placeholder-shown:-translate-y-1/2 peer-placeholder-shown:top-1/2 peer-focus:top-2 peer-focus:scale-75 peer-focus:-translate-y-4 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto start-1',
  LABEL_DEFAULT_ERROR: 'block mb-2 text-sm font-medium text-gray-900 dark:text-red-700',
  COLS_1: 'md:grid-cols-1',
  COLS_2: 'md:grid-cols-2',
  COLS_3: 'md:grid-cols-3',
  COLS_4: 'md:grid-cols-4',
  DATE_TIME_PICKER_TODAY:
    'border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full ps-10 p-2.5 px-2.5 pb-2.5 pt-4 my-3 dark:bg-gray-900 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500',
  BUTTON_GRADIENT_1:
    'text-gray-900 bg-gradient-to-r from-teal-200 to-lime-200 hover:bg-gradient-to-l hover:from-teal-200 hover:to-lime-200 focus:ring-4 focus:outline-none focus:ring-lime-200 dark:focus:ring-teal-700 font-medium rounded-lg text-sm px-5 py-2.5 text-center me-2 mb-2',
  BUTTON_GRADIENT_2:
    'text-gray-900 bg-gradient-to-r from-red-200 via-red-300 to-yellow-200 hover:bg-gradient-to-bl focus:ring-4 focus:outline-none focus:ring-red-100 dark:focus:ring-red-400 font-medium rounded-lg text-sm px-5 py-2.5 text-center me-2 mb-2',
  BUTTON_DEFAULT_WHITE:
    'text-gray-900 bg-white hover:bg-gray-100 border border-gray-200 focus:ring-4 focus:outline-none focus:ring-gray-100 font-medium rounded-lg text-sm px-5 py-2.5 text-center inline-flex items-center dark:focus:ring-gray-600 dark:bg-gray-800 dark:border-gray-700 dark:text-white dark:hover:bg-gray-700 me-2 mb-2',
  BUTTON_DEFAULT_BLUE_1:
    'text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-4 py-2 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800',
  BUTTON_DEFAULT_BLUE_2:
    'text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-4 py-2 dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800',
  BUTTON_DEFAULT_GRAY_1: 'bg-gray-500 text-white px-4 py-2 rounded-lg hover:bg-gray-600',
  BUTTON_GRADIENT_BLUE_1:
    'text-white bg-gradient-to-r from-blue-500 via-blue-600 to-blue-700 hover:bg-gradient-to-br font-medium rounded-lg text-sm px-5 py-2.5 text-center me-2 mb-2',
  BUTTON_RED_1:
    'focus:outline-none text-white bg-red-700 hover:bg-red-800 focus:ring-4 focus:ring-red-300 font-medium rounded-lg text-sm px-5 py-2.5 me-2 mb-2 dark:bg-red-600 dark:hover:bg-red-700 dark:focus:ring-red-900',
  SELECT_INPUT:
    'bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500',
}
