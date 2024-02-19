import { observer } from "mobx-react-lite";
import { useState } from "react";
import "../../../../css/create.css";

export const FirsStep = observer(
  ({
    quizModelJson,
    setQuizModelJson,
  }: {
    quizModelJson: any;
    setQuizModelJson:React.Dispatch<React.SetStateAction<any>>;
  }) => {
    const [title, setTitle] = useState(quizModelJson.title);
    const [description, setDescription] = useState(
      quizModelJson.quizDescription
    );

    const handleAddTag = (e: React.KeyboardEvent<HTMLInputElement>) => {
      const target = e.target as HTMLInputElement;
      const value = target.value;
      if (e.key === "Enter" && value.trim() !== "") {

        setQuizModelJson({
          ...quizModelJson,
          tags:[...quizModelJson.tags,value.trim()]
        });
   
        target.value = "";
      }
    };

    const removeTag = (indexToRemove: number) => {
      setQuizModelJson({
        ...quizModelJson,
        tags:quizModelJson.tags.filter((_: any, index: number) => index !== indexToRemove)
      });
    };

    const handleTitleChange = (e: { target: { value: any } }) => {
      setTitle(e.target.value);
      quizModelJson.title = e.target.value;
    };

    const handleDescriptionChange = (e: { target: { value: any } }) => {
      setDescription(e.target.value);
      quizModelJson.quizDescription = e.target.value;
    };

    return (
      <>
        <div>
          <input
            type="text"
            value={title}
            onChange={handleTitleChange}
            placeholder="Quiz title"
          />
          <input
            type="text"
            value={description}
            onChange={handleDescriptionChange}
            placeholder="Description"
          />
        </div>

        <input type="file" placeholder="titleImg" />

        <div className="d-block">
          <p className="text-center mt-4">Tags</p>
          {quizModelJson.tags.map((tag:string[], index:number) => (
            <div className="m-0">
              <li key={index}>
                <div className="col">
                  <h4>{tag}</h4>
                </div>
                <div className="col">
                  <span onClick={() => removeTag(index)}>{" Remove"}</span>
                  <span onClick={() => removeTag(index)}>{" Edit"}</span>
                </div>
              </li>
            </div>
          ))}

          <input
            type="text"
            placeholder="Enter a tag and press Enter"
            onKeyDown={handleAddTag}
          />

          <select
            defaultValue={(quizModelJson.type = "ForFun")}
            value={quizModelJson.type}
            onChange={(e) => (quizModelJson.type = e.target.value)}
          >
            <option value="ForFun">For fun</option>
            <option value="Learning">Learning</option>
            <option value="Psychological">Psychological</option>
          </select>
        </div>
      </>
    );
  }
);
