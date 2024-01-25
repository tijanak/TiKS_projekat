//import { defineConfig } from '@playwright/test';

//export default defineConfig({
//  webServer: [
//    {
//      command: 'npm run start',
//      url: 'http://127.0.0.1:3000',
//      timeout: 120 * 1000,
//      reuseExistingServer: !process.env.CI,
//    },
//    {
//command: 'npm run backend',
//      url: 'http://127.0.0.1:3333',
//      timeout: 120 * 1000,
//      reuseExistingServer: !process.env.CI,
//    }
//  ],
//  use:
//{
//baseURL: 'http://127.0.0.1:3000',
//  },
//});