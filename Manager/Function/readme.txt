        /*
        //IEccCmd接口实现时使用异步执行的示例
        private delegate void Example(ExampleCmdArgs args);
        private void EccCmd_Example(ExampleCmdArgs args)
        {
           //do somethings... 
        }
        void IEccCmd.EccCmd_Example(ExampleCmdArgs args, AsyncCallback callback)
        {
            ((Example)EccCmd_Example).BeginInvoke(args, callback, null);
        }
        */