// @ts-check
// Note: type annotations allow type checking and IDEs autocompletion

const lightCodeTheme = require('prism-react-renderer/themes/github');
const darkCodeTheme = require('prism-react-renderer/themes/dracula');

/** @type {import('@docusaurus/types').Config} */
const config = {
  title: 'Kiva Partner API',
  tagline: 'Placing your loans in the Kiva Marketplace',
  url: 'https://fps-sdk-portal.web.app',
  baseUrl: '/',
  onBrokenLinks: 'warn',
  onBrokenMarkdownLinks: 'warn',
  favicon: 'https://www-kiva-org.global.ssl.fastly.net/REVISION/img/favicon/favicon.ico',

  // GitHub pages deployment config.
  // If you aren't using GitHub pages, you don't need these.
  organizationName: 'Kiva', // Usually your GitHub org/user name.
  projectName: 'Partner API', // Usually your repo name.

  // Even if you don't use internalization, you can use this field to set useful
  // metadata like html lang. For example, if your site is Chinese, you may want
  // to replace "en" with "zh-Hans".
  i18n: {
    defaultLocale: 'en',
    locales: ['en', 'es', 'fr'],
  },

  presets: [
    [
      'classic',
      /** @type {import('@docusaurus/preset-classic').Options} */
      ({
        docs: {
          sidebarPath: require.resolve('./sidebars.js'),
        },
        blog: {
          showReadingTime: true,
        },
        theme: {
          customCss: require.resolve('./src/css/custom.css'),
        },
      }),
    ],
  ],

  themeConfig:
    /** @type {import('@docusaurus/preset-classic').ThemeConfig} */
    ({
      navbar: {
        title: 'Kiva Partner API Docs',
        logo: {
          alt: 'Kiva Partner API Docs',
          src: 'img/logo.svg',
        },
        items: [
           /* this language menu doesn't work right */
          {
            type: 'localeDropdown',
            position: 'left',
          },
          {
            type: 'doc',
            docId: 'overview/overview',
            position: 'left',
            label: 'Introduction',
          },
          {href: 'https://partner-api.k1.kiva.org/swagger-ui', label: 'API Reference', position: 'left'},
          {
            href: 'https://github.com/Kiva/fsp-sdk',
            label: 'SDKs',
            position: 'right',
          },
          {
            href: 'https://kiva.tfaforms.net/107',
            label: 'Start Your Integration',
            position: 'right',
          },
        ],
      },
      footer: {
        style: 'dark',
        links: [
          {
            title: 'Docs',
            items: [
              {
                label: 'Tutorial',
                to: '/docs/overview',
              },
            ],
          },
          {
            title: 'Guides',
            items: [
              {
                label: 'File a Bug',
                to: '/docs/guides/bug',
              },
            ],
          },
          {
            title: 'More',
            items: [
              {
                label: 'GitHub',
                href: 'https://github.com/Kiva/fps-sdk',
              },
            ],
          },
        ],
        copyright: `Copyright © ${new Date().getFullYear()} Kiva Microfunds, Inc. `,
      },
      prism: {
        theme: lightCodeTheme,
        darkTheme: darkCodeTheme,
      },
    }),
};

module.exports = config;
