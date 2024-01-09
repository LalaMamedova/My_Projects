import React, { useContext } from 'react';
import { Breadcrumb } from 'react-bootstrap';
import { observer } from 'mobx-react-lite';
import { Context } from '..';
import "../UserPath/css/index.css"
const BreadcrumbsWrapper = observer(() => {
  const {breadPath} = useContext(Context);

  return (
    <Breadcrumb className="mt-1">
      {breadPath &&
        breadPath.Paths.map((path, index) => (
          <Breadcrumb.Item  key={index} href={path.url}>
            {path.name}
          </Breadcrumb.Item>
        ))}
    </Breadcrumb>
  );
});

export default BreadcrumbsWrapper;
