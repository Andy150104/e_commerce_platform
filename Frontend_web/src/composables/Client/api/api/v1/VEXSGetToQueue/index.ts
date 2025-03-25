/* eslint-disable */
import type { DefineMethods } from 'aspida';
import type * as Types from '../../../@types';

export type Methods = DefineMethods<{
  get: {
    query?: {
      ExchangeId?: string | undefined;
      SearchName?: string | null | undefined;
    } | undefined;

    status: 200;
    resBody: Types.VEXSGetToQueueResponse;
  };
}>;
