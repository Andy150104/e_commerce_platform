import type { AspidaClient, BasicHeaders } from 'aspida'
import { dataToURLString } from 'aspida'
import type { Methods as Methods_12v90hq } from './api'
import type { Methods as Methods_10dhobp } from './api/d'
import type { Methods as Methods_ni9l0r } from './api/d/_code@number'
import type { Methods as Methods_12ycs32 } from './api/d/search'
import type { Methods as Methods_13pcwnd } from './api/p'
import type { Methods as Methods_1ew62y7 } from './api/p/_code@number'
import type { Methods as Methods_rzey7u } from './api/p/search'
import type { Methods as Methods_yqfb6l } from './api/version'
import type { Methods as Methods_13zcicc } from './api/w'
import type { Methods as Methods_duyr6g } from './api/w/_code@number'
import type { Methods as Methods_1liygwl } from './api/w/search'

const api = <T>({ baseURL, fetch }: AspidaClient<T>) => {
  const prefix = (baseURL === undefined ? '' : baseURL).replace(/\/$/, '')
  const PATH0 = '/api'
  const PATH1 = '/api/d'
  const PATH2 = '/api/d/search'
  const PATH3 = '/api/p'
  const PATH4 = '/api/p/search'
  const PATH5 = '/api/version'
  const PATH6 = '/api/w'
  const PATH7 = '/api/w/search'
  const GET = 'GET'

  return {
    api: {
      d: {
        _code: (val2: number) => {
          const prefix2 = `${PATH1}/${val2}`

          return {
            /**
             * @returns Successful Response
             */
            get: (option?: { query?: Methods_ni9l0r['get']['query'] | undefined; config?: T | undefined } | undefined) =>
              fetch<Methods_ni9l0r['get']['resBody'], BasicHeaders, Methods_ni9l0r['get']['status']>(prefix, prefix2, GET, option).json(),
            /**
             * @returns Successful Response
             */
            $get: (option?: { query?: Methods_ni9l0r['get']['query'] | undefined; config?: T | undefined } | undefined) =>
              fetch<Methods_ni9l0r['get']['resBody'], BasicHeaders, Methods_ni9l0r['get']['status']>(prefix, prefix2, GET, option)
                .json()
                .then((r) => r.body),
            $path: (option?: { method?: 'get' | undefined; query: Methods_ni9l0r['get']['query'] } | undefined) =>
              `${prefix}${prefix2}${option && option.query ? `?${dataToURLString(option.query)}` : ''}`,
          }
        },
        search: {
          /**
           * @returns Successful Response
           */
          get: (option: { query: Methods_12ycs32['get']['query']; config?: T | undefined }) =>
            fetch<Methods_12ycs32['get']['resBody'], BasicHeaders, Methods_12ycs32['get']['status']>(prefix, PATH2, GET, option).json(),
          /**
           * @returns Successful Response
           */
          $get: (option: { query: Methods_12ycs32['get']['query']; config?: T | undefined }) =>
            fetch<Methods_12ycs32['get']['resBody'], BasicHeaders, Methods_12ycs32['get']['status']>(prefix, PATH2, GET, option)
              .json()
              .then((r) => r.body),
          $path: (option?: { method?: 'get' | undefined; query: Methods_12ycs32['get']['query'] } | undefined) =>
            `${prefix}${PATH2}${option && option.query ? `?${dataToURLString(option.query)}` : ''}`,
        },
        /**
         * @returns Successful Response
         */
        get: (option?: { config?: T | undefined } | undefined) =>
          fetch<Methods_10dhobp['get']['resBody'], BasicHeaders, Methods_10dhobp['get']['status']>(prefix, PATH1, GET, option).json(),
        /**
         * @returns Successful Response
         */
        $get: (option?: { config?: T | undefined } | undefined) =>
          fetch<Methods_10dhobp['get']['resBody'], BasicHeaders, Methods_10dhobp['get']['status']>(prefix, PATH1, GET, option)
            .json()
            .then((r) => r.body),
        $path: () => `${prefix}${PATH1}`,
      },
      p: {
        _code: (val2: number) => {
          const prefix2 = `${PATH3}/${val2}`

          return {
            /**
             * @returns Successful Response
             */
            get: (option?: { query?: Methods_1ew62y7['get']['query'] | undefined; config?: T | undefined } | undefined) =>
              fetch<Methods_1ew62y7['get']['resBody'], BasicHeaders, Methods_1ew62y7['get']['status']>(prefix, prefix2, GET, option).json(),
            /**
             * @returns Successful Response
             */
            $get: (option?: { query?: Methods_1ew62y7['get']['query'] | undefined; config?: T | undefined } | undefined) =>
              fetch<Methods_1ew62y7['get']['resBody'], BasicHeaders, Methods_1ew62y7['get']['status']>(prefix, prefix2, GET, option)
                .json()
                .then((r) => r.body),
            $path: (option?: { method?: 'get' | undefined; query: Methods_1ew62y7['get']['query'] } | undefined) =>
              `${prefix}${prefix2}${option && option.query ? `?${dataToURLString(option.query)}` : ''}`,
          }
        },
        search: {
          /**
           * @returns Successful Response
           */
          get: (option: { query: Methods_rzey7u['get']['query']; config?: T | undefined }) =>
            fetch<Methods_rzey7u['get']['resBody'], BasicHeaders, Methods_rzey7u['get']['status']>(prefix, PATH4, GET, option).json(),
          /**
           * @returns Successful Response
           */
          $get: (option: { query: Methods_rzey7u['get']['query']; config?: T | undefined }) =>
            fetch<Methods_rzey7u['get']['resBody'], BasicHeaders, Methods_rzey7u['get']['status']>(prefix, PATH4, GET, option)
              .json()
              .then((r) => r.body),
          $path: (option?: { method?: 'get' | undefined; query: Methods_rzey7u['get']['query'] } | undefined) =>
            `${prefix}${PATH4}${option && option.query ? `?${dataToURLString(option.query)}` : ''}`,
        },
        /**
         * @returns Successful Response
         */
        get: (option?: { config?: T | undefined } | undefined) =>
          fetch<Methods_13pcwnd['get']['resBody'], BasicHeaders, Methods_13pcwnd['get']['status']>(prefix, PATH3, GET, option).json(),
        /**
         * @returns Successful Response
         */
        $get: (option?: { config?: T | undefined } | undefined) =>
          fetch<Methods_13pcwnd['get']['resBody'], BasicHeaders, Methods_13pcwnd['get']['status']>(prefix, PATH3, GET, option)
            .json()
            .then((r) => r.body),
        $path: () => `${prefix}${PATH3}`,
      },
      version: {
        /**
         * @returns Successful Response
         */
        get: (option?: { config?: T | undefined } | undefined) =>
          fetch<Methods_yqfb6l['get']['resBody'], BasicHeaders, Methods_yqfb6l['get']['status']>(prefix, PATH5, GET, option).json(),
        /**
         * @returns Successful Response
         */
        $get: (option?: { config?: T | undefined } | undefined) =>
          fetch<Methods_yqfb6l['get']['resBody'], BasicHeaders, Methods_yqfb6l['get']['status']>(prefix, PATH5, GET, option)
            .json()
            .then((r) => r.body),
        $path: () => `${prefix}${PATH5}`,
      },
      w: {
        _code: (val2: number) => {
          const prefix2 = `${PATH6}/${val2}`

          return {
            /**
             * @returns Successful Response
             */
            get: (option?: { config?: T | undefined } | undefined) =>
              fetch<Methods_duyr6g['get']['resBody'], BasicHeaders, Methods_duyr6g['get']['status']>(prefix, prefix2, GET, option).json(),
            /**
             * @returns Successful Response
             */
            $get: (option?: { config?: T | undefined } | undefined) =>
              fetch<Methods_duyr6g['get']['resBody'], BasicHeaders, Methods_duyr6g['get']['status']>(prefix, prefix2, GET, option)
                .json()
                .then((r) => r.body),
            $path: () => `${prefix}${prefix2}`,
          }
        },
        search: {
          /**
           * @returns Successful Response
           */
          get: (option: { query: Methods_1liygwl['get']['query']; config?: T | undefined }) =>
            fetch<Methods_1liygwl['get']['resBody'], BasicHeaders, Methods_1liygwl['get']['status']>(prefix, PATH7, GET, option).json(),
          /**
           * @returns Successful Response
           */
          $get: (option: { query: Methods_1liygwl['get']['query']; config?: T | undefined }) =>
            fetch<Methods_1liygwl['get']['resBody'], BasicHeaders, Methods_1liygwl['get']['status']>(prefix, PATH7, GET, option)
              .json()
              .then((r) => r.body),
          $path: (option?: { method?: 'get' | undefined; query: Methods_1liygwl['get']['query'] } | undefined) =>
            `${prefix}${PATH7}${option && option.query ? `?${dataToURLString(option.query)}` : ''}`,
        },
        /**
         * @returns Successful Response
         */
        get: (option?: { config?: T | undefined } | undefined) =>
          fetch<Methods_13zcicc['get']['resBody'], BasicHeaders, Methods_13zcicc['get']['status']>(prefix, PATH6, GET, option).json(),
        /**
         * @returns Successful Response
         */
        $get: (option?: { config?: T | undefined } | undefined) =>
          fetch<Methods_13zcicc['get']['resBody'], BasicHeaders, Methods_13zcicc['get']['status']>(prefix, PATH6, GET, option)
            .json()
            .then((r) => r.body),
        $path: () => `${prefix}${PATH6}`,
      },
      /**
       * @returns Successful Response
       */
      get: (option?: { query?: Methods_12v90hq['get']['query'] | undefined; config?: T | undefined } | undefined) =>
        fetch<Methods_12v90hq['get']['resBody'], BasicHeaders, Methods_12v90hq['get']['status']>(prefix, PATH0, GET, option).json(),
      /**
       * @returns Successful Response
       */
      $get: (option?: { query?: Methods_12v90hq['get']['query'] | undefined; config?: T | undefined } | undefined) =>
        fetch<Methods_12v90hq['get']['resBody'], BasicHeaders, Methods_12v90hq['get']['status']>(prefix, PATH0, GET, option)
          .json()
          .then((r) => r.body),
      $path: (option?: { method?: 'get' | undefined; query: Methods_12v90hq['get']['query'] } | undefined) =>
        `${prefix}${PATH0}${option && option.query ? `?${dataToURLString(option.query)}` : ''}`,
    },
  }
}

export type ApiInstance = ReturnType<typeof api>
export default api
