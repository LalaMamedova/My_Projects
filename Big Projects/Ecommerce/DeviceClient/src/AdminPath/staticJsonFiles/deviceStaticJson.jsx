export function subCategoryCharacteristicsJson(subCategoryId,name = '',description='') {
  const chars = 
  {
    "id":0,
    "name": name,
    "subCategory": null,
    "description": description,
    "subCategoryId": subCategoryId,
  }
  return chars;
}

  export function subCategoryJson(id,name, categoryId,icon,characteristics) {
    const subCategory = 
    {
      "id":id,
      "name": name,
      "categoryId": categoryId,
      "icon":icon,
      "characteristics": characteristics,
    }
    return subCategory;
  }

  
  export function productJson(name,price,
    brandId,subCategoryId,
    productImg,productOriginalImg, 
    productСharacteristics)
    {
      const product = {
      "id":0,
      "name": name,
      "price": price,
      "brandId": brandId,
      "subCategoryId": subCategoryId,
      "productsImg": productImg,
      "originalImgs":productOriginalImg,
      "productСharacteristics": productСharacteristics,
    }
  return product;
}

export function categoryJson(id,name,icon,
  subcategoryName,subcategoryIcon, 
  categoryId,characteristics){
    const category = {
      "id":id,
      "name": name,
      "icon": icon,
      "subCategories": [
      {
        "name": subcategoryName,
        "icon": subcategoryIcon,
        "categoryId": categoryId,
        "characteristics": characteristics,
      }
    ]
  }
  return category;
}


export function productCharsJson(charId, value){
    const chars = {
      "characteristicId": charId,
      "value": value
  };
  return chars;
}

export function reviewJson(rating, userId,ProductId){
  const review = {
    "id": 0,
    "rating": rating,
    "appUserId": userId,
    "productId": ProductId,
  };
  return review;
}

export function likedProductJson(userId, productId){
  const likedProduct = {
    "id": 0,
    "productId": userId,
    "appUserId": productId
  };
  return likedProduct;
}
export function purchasedProductJson(userId, productId,totalSum){
  const likedProduct = {
    "id": 0,
    "appUserId": userId,
    "productId": productId,
    "totalSum": totalSum
  };
  return likedProduct;
}