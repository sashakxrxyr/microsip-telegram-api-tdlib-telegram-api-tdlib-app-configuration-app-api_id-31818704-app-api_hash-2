export const NATIVE_LIB_PATH = {
  tdjson: require.resolve('./native/tdjson.dll'),
  libcrypto: require.resolve('./native/libcrypto-3-x64.dll'),
  libssl: require.resolve('./native/libssl-3-x64.dll'),
  zlib: require.resolve('./native/zlib1.dll'),
};

export const NATIVE_DIR = __dirname;