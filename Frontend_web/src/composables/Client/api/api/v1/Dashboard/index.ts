/* eslint-disable */
import type { DefineMethods } from 'aspida';
import type * as Types from '../../../@types';

export type Methods = DefineMethods<{
  get: {
    query?: {
      Month?: number | undefined;
      Year?: number | undefined;
      Date?: string | undefined;
    } | undefined;

    status: 200;
    resBody: Types.DashboardResponse;
  };
}>;
