module.exports = {
  extends: ['next', 'airbnb', 'airbnb-typescript', 'airbnb/hooks', 'prettier'],
  rules: {
    '@next/next/no-html-link-for-pages': 'off',
    '@typescript-eslint/consistent-type-imports': 'error',
    'react/jsx-props-no-spreading': 'off',
    'react/prop-types': 'off',
    'react/function-component-definition': [
      2,
      { namedComponents: 'arrow-function', unnamedComponents: 'arrow-function' },
    ],
    'sort-imports': [
      'error',
      {
        ignoreDeclarationSort: true,
        ignoreMemberSort: false,
      },
    ],
    'import/extensions': 'off',
    'import/order': [
      'error',
      {
        'newlines-between': 'always',
        groups: ['type', 'builtin', 'external', 'parent', 'sibling', 'internal', 'index', 'object'],
        alphabetize: { order: 'asc', caseInsensitive: true },
        pathGroupsExcludedImportTypes: [],
        pathGroups: [
          {
            pattern: '@giantnodes/**',
            group: 'external',
            position: 'after',
          },
        ],
      },
    ],
  },
}
