function getPathKeyFromObject(obj: object, path: string, key: string): string {
  return Array.isArray(obj) ? `${path}[${key}]` : `${path}.${key}`;
}

export const flatten = (object: any) => {
  const o = Object.assign({}, ...function _flatten(objectBit, path = ""): any {
    return [].concat(
      ...Object.keys(objectBit).map(
        key => objectBit[key] && typeof objectBit[key] === "object" && !(objectBit[key] instanceof Date)
          ? _flatten(
            objectBit[key],
            getPathKeyFromObject(objectBit, path, key)
          )
          : ({ [getPathKeyFromObject(objectBit, path, key)]: (objectBit[key] == null
              ? ""
              : (objectBit[key] instanceof Date
                ? (objectBit[key] as Date).toUTCString()
                : objectBit[key])) })
      )
    );
  }(object));
  return Object.assign({}, ...function _sliceFirst(obj) {
    return [].concat(...Object.keys(o).map((key: string) => ({ [key.slice(1)]: o[key] })) as any[]);
  }(o));
};
