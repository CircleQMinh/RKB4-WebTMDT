import React, { Fragment } from "react";

function Pagination(props) {
  //console.log("props", props)
  //console.log(typeof(props.pageNumber))
  let pagi = [...Array(props.totalPage)].map((e, i) => {
    //  console.log(typeof(i+1))
    return (
      <button
        onClick={props.onPageNumberChange}
        className={
          props.pageNumber == i + 1
            ? "paginationsa:number paginationsa_active"
            : "paginationsa:number"
        }
        key={i}
        value={i + 1}
      >
        {i + 1}
      </button>
    );
  });

  return (
    <Fragment>

      {pagi}

    </Fragment>
  );
}
export default Pagination;