using System;

public interface Irepository
{
	
        public void Create(Object model);
        public void Get(Object model);
        public void GetById(Object model);
        public void Delete(Object model);
        public void Update(Object model);
    
}
